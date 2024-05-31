using Rubin.Static.Models;
using Rubin.Static.ViewModels;

namespace Rubin.Static.Rendering;

/// <summary>
/// This type implements pretty straight forward methods for rendering and saving 
/// static content. Each method points to a View and might also take a Model. 
/// Each of these generator methods can also save its content.
/// </summary>
public class PageRendering : IRenderPages
{
    private readonly IRenderer _renderer;
    private const string CategoryViewName = "Category";
    private const string PostViewName = "Post";
    private const string IndexViewName = "Index";
    private const string AllViewName = "All";

    public PageRendering(IRenderer renderer)
    {
        _renderer = renderer;
    }

    public async Task<RenderedPage> IndexPage(IEnumerable<Post> posts)
    {
        var model = new IndexPageViewModel(posts);

        var html = await _renderer.RenderViewToPageAsync(IndexViewName, model);

        return new RenderedPage(new Slug(IndexViewName.ToLower()), html);
    }

    public async Task<RenderedPage> AllPage(IEnumerable<Post> posts)
    {
        var model = new AllPageViewModel(posts);

        var html = await _renderer.RenderViewToPageAsync(AllViewName, model);

        return new RenderedPage(new Slug(AllViewName.ToLower()), html);
    }

    public async Task<RenderedPage> CategoryPage(Category category, IEnumerable<Post> posts)
    {
        var model = new CategoryPageViewModel(category, posts);

        var html = await _renderer.RenderViewToPageAsync(CategoryViewName, model);

        return new RenderedPage(category.Slug, html);
    }

    public async Task<IEnumerable<RenderedPage>> CategoryPage(Dictionary<Category, IEnumerable<Post>> posts)
    {
        var renderings = new List<RenderedPage>();

        foreach (var post in posts)
        {
            renderings.Add(
                await CategoryPage(post.Key, post.Value)
            );
        }

        return renderings.AsEnumerable();
    }

    public async Task<RenderedPage> PostPage(Post post)
    {
        var model = new PostPageViewModel(post);

        var html = await _renderer.RenderViewToPageAsync(PostViewName, model);

        return new RenderedPage(post.Slug, html);
    }

    public async Task<IEnumerable<RenderedPage>> PostPage(IEnumerable<Post> posts)
    {
        var renderings = new List<RenderedPage>();

        foreach (var post in posts)
        {
            renderings.Add(
                await PostPage(post)
            );
        }

        return renderings;
    }
}
