using Rubin.Static.Models;

namespace Rubin.Static.Services;

public interface ICategoryPostOrganizer
{
    /// <summary>
    /// Returns a dictionary with distinct Category as key and Posts in given Category as Values
    /// If a Post has several Categories it is present in all those Categories
    /// </summary>
    /// <param name="categories"></param>
    /// <param name="posts"></param>
    /// <returns></returns>
    Dictionary<Category, IEnumerable<Post>> GetCategoryPosts(IEnumerable<Post> posts);
    IEnumerable<Post> GetNoCategoryPosts(IEnumerable<Post> posts);
    IEnumerable<Post> GetPostsInCategory(Category category, IEnumerable<Post> posts);
}
