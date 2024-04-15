using Rubin.Static.Models;

namespace Rubin.Static.Extensions;
public static class CategoryPostExtensions
{
    public static Dictionary<Category, IEnumerable<Post>> OrderByPublishDate(this Dictionary<Category, IEnumerable<Post>> posts)
    {
        var orderedDict = posts.ToDictionary(
            pair => pair.Key,
            pair => pair.Value.OrderByDescending(post => post.PublishedDate).AsEnumerable());

        return orderedDict;
    }

    public static IEnumerable<Post> OrderByPublishDate(this IEnumerable<Post> posts)
    {
        var orderedPosts = posts.OrderByDescending(post => post.PublishedDate).AsEnumerable();

        return orderedPosts;
    }
}
