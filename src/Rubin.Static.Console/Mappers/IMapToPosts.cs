namespace Rubin.Static.Console.Mappers;

public interface IMapToPosts
{
    /// <summary>
    /// Implicitly calling outwards for getting its needed Markdown data.
    /// </summary>
    /// <returns>Mapped Posts</returns>
    Task<IEnumerable<Post>> MapToPostsAsync();

    /// <returns>Mapped Posts from given markdown posts</returns>
    Task<IEnumerable<Post>> MapToPostsAsync(IEnumerable<MarkdownPost> markdownPosts);
}
