using Rubin.Static.Models;

namespace Rubin.Static.Services;

public interface IGenerateStatic
{
    Task<string> IndexPage(IEnumerable<Post> posts, Func<string, Slug, Task<string>> fileSavingFunc);
    Task<string> CategoryPage(Category category, IEnumerable<Post> posts, Func<string, Slug, Task<string>> fileSavingFunc);
    Task<string> CategoryPage(Dictionary<Category, IEnumerable<Post>> posts, Func<string, Slug, Task<string>> fileSavingFunc);
    Task<string> PostPage(IEnumerable<Post> posts, Func<string, Slug, Task<string>> fileSavingFunc);
    Task<string> AllPage(IEnumerable<Post> posts, Func<string, Slug, Task<string>> fileSavingFunc);
}
