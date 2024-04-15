using Rubin.Static.Models;

namespace Rubin.Static.Infrastructure;

/// <summary>
/// A static singleton helper type that is meant for use from the Layout view due to the 
/// weak enablement of passing a strongly typed model to a Layout view. 
/// 
/// Another approach for passing a strongly type to the Layout would have been to add a property
/// to each ViewModel with the wanted data to be passed to the Layout, and from each View setting a
/// ViewData data-bag which would then be picked up by the Layout. But as you might have already
/// understood, it would be less clean and from my developments point of view, a poor choice.
/// 
/// So static singleton it is.
/// </summary>
public class LayoutHelper
{
    private static readonly Lazy<LayoutHelper> instance = new Lazy<LayoutHelper>(() => new LayoutHelper());
    public static LayoutHelper Instance => instance.Value;

    private IEnumerable<Post> posts = new List<Post>();
    private Dictionary<Category, IEnumerable<Post>> categoryPosts = new Dictionary<Category, IEnumerable<Post>>();

    private LayoutHelper() { }

    public async Task Initialize(IEnumerable<Post> posts)
    {
        ArgumentNullException.ThrowIfNull(posts);
        this.posts = posts;

        // new the instance if called again...and again
        categoryPosts = new Dictionary<Category, IEnumerable<Post>>();

        await Task.CompletedTask;
    }

    /// <summary>
    /// This can be called from the Layout view if one wish to render each Category with e.g. a count of posts in that category.
    /// </summary>
    /// <returns></returns>
    public async Task<Dictionary<Category, IEnumerable<Post>>> GetCategoryPosts()
    {
        if (categoryPosts.Count > 0) return categoryPosts;

        var distinctCategories = posts.SelectMany(post => post.Categories)
            .Distinct(new CategoryTitleComparer())
            .ToList();

        var tempCategoryPosts = new Dictionary<Category, IEnumerable<Post>>();

        foreach (var category in distinctCategories)
        {
            var postsInCategory = posts.Where
                (p => p.Categories.Contains(category, new CategoryTitleComparer())).ToList();

            if (postsInCategory.Any())
            {
                tempCategoryPosts.Add(
                    new Category
                    {
                        Title = category.Title.Trim()
                    }, postsInCategory);
            }
        }

        categoryPosts = tempCategoryPosts;

        return await Task.FromResult(categoryPosts);
    }
}
