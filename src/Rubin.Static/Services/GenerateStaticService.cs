using Rubin.Static.Infrastructure;
using Rubin.Static.Models;
using Rubin.Static.ViewModels;

namespace Rubin.Static.Services;

/// <summary>
/// This type implements pretty straight forward methods for rendering and saving 
/// static content. Each generator method points to a View and might also take a Model. 
/// Each of these generator methods can also save its content.
/// </summary>
public class GenerateStaticService : IGenerateStatic
{
    private readonly IRenderer _renderer;
    private const string CategoryViewName = "Category";
    private const string PostViewName = "Post";
    private const string IndexViewName = "Index";
    private const string AllViewName = "All";

    public GenerateStaticService(IRenderer renderer)
    {
        _renderer = renderer;
    }

    public async Task<string> IndexPage(IEnumerable<Post> posts, Func<string, Slug, Task<string>> fileSavingFunc)
    {
        var model = new IndexPageViewModel(posts);

        var html = await _renderer.RenderViewToStringAsync(IndexViewName, model);

        return await fileSavingFunc.Invoke(html, new Slug(IndexViewName.ToLower()));
    }

    public async Task<string> AllPage(IEnumerable<Post> posts, Func<string, Slug, Task<string>> fileSavingFunc)
    {
        var model = new AllPageViewModel(posts);

        var html = await _renderer.RenderViewToStringAsync(AllViewName, model);

        return await fileSavingFunc.Invoke(html, new Slug(AllViewName.ToLower()));
    }

    public async Task<string> CategoryPage(Category category, IEnumerable<Post> posts, Func<string, Slug, Task<string>> fileSavingFunc)
    {
        var model = new CategoryPageViewModel(category, posts);

        var html = await _renderer.RenderViewToStringAsync(CategoryViewName, model);

        return await fileSavingFunc.Invoke(html, category.Slug);
    }

    public async Task<string> CategoryPage(Dictionary<Category, IEnumerable<Post>> posts, Func<string, Slug, Task<string>> fileSavingFunc)
    {
        foreach (var post in posts)
        {
            await CategoryPage(post.Key, post.Value, fileSavingFunc);
        }

        return string.Empty;
    }

    public async Task<string> PostPage(Post post, Func<string, Slug, Task<string>> fileSavingFunc)
    {
        var model = new PostPageViewModel(post);

        var html = await _renderer.RenderViewToStringAsync(PostViewName, model);

        return await fileSavingFunc.Invoke(html, post.Slug);
    }

    public async Task<string> PostPage(IEnumerable<Post> posts, Func<string, Slug, Task<string>> fileSavingFunc)
    {
        var result = string.Empty;

        foreach (var item in posts)
        {
            result += await PostPage(item, fileSavingFunc);
        }

        return result;
    }
}
