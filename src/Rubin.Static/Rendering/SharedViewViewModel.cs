using Rubin.Static.Models;

namespace Rubin.Static.Rendering;

/// <summary>
/// A static singleton helper type that is meant for use from a Shared View (e.g. Layout) due to the 
/// weak enablement of passing a strongly typed model to a Shared View (e.g. Layout). 
/// 
/// Historic considerations: Another approach for passing a strongly type to the Layout would have been to 
/// add a property to each ViewModel with the wanted data to be passed to the Layout, and from each View 
/// setting a ViewData data-bag which would then be picked up by the Layout. But as you might have already
/// understood, it would be less clean and from my developments point of view, a poor choice.
/// 
/// So static singleton of a Model it is.
/// </summary>
public class SharedViewViewModel
{
    private static readonly Lazy<SharedViewViewModel> instance = new Lazy<SharedViewViewModel>(() => new SharedViewViewModel());
    public static SharedViewViewModel Instance => instance.Value;

    private IEnumerable<Post> posts = new List<Post>();
    private Dictionary<Category, IEnumerable<Post>> categoryPosts = new Dictionary<Category, IEnumerable<Post>>();

    private SharedViewViewModel() { }

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
