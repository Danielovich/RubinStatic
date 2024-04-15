using Rubin.Static.Models;

namespace Rubin.Static.Services;

public class CategoryPostsService : ICategoryPostOrganizer
{
    public IEnumerable<Post> GetPostsInCategory(Category category, IEnumerable<Post> posts)
    {
        return posts.Where(post => post.Categories.Contains(category, new CategoryTitleComparer()));
    }

    /// <summary>
    /// Returns a dictionary with distinct Category as key and Posts in given Category as Values
    /// If a Post has several Categories it is present in all those Categories
    /// </summary>
    /// <param name="categories"></param>
    /// <param name="posts"></param>
    /// <returns></returns>
    public Dictionary<Category, IEnumerable<Post>> GetCategoryPosts(IEnumerable<Post> posts)
    {
        var distinctCategories = posts.SelectMany(c => c.Categories)
            .Distinct(new CategoryTitleComparer())
            .Select(c => new Category()
            {
                Title = c.Title.Trim()
            }).ToList();

        var result = new Dictionary<Category, IEnumerable<Post>>();

        foreach (var category in distinctCategories)
        {
            var postsInCategory = posts.Where(p => p.Categories.Contains(category, new CategoryTitleComparer())).ToList();

            if (postsInCategory.Any())
            {
                result.Add(category, postsInCategory);
            }
        }

        return result;
    }

    public IEnumerable<Post> GetNoCategoryPosts(IEnumerable<Post> posts)
    {
        var result = posts.Where(p => !p.Categories.Any()).ToList();

        return result;
    }
}
