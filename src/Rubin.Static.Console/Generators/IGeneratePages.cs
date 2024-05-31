namespace Rubin.Markdown.Console.Generators;

public interface IGeneratePages
{
    Task GenerateCategoryPage(IEnumerable<Post> posts);
    Task GeneratePostPage(IEnumerable<Post> posts);
    Task GenerateIndexPage(IEnumerable<Post> posts, int showNumberOfPosts = 10);
    Task GenerateAllPage(IEnumerable<Post> posts);
    Task GenerateNoCategoryPage(IEnumerable<Post> posts, string uncategorizedTitle);
}
