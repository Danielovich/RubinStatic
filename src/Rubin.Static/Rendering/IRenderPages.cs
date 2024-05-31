using Rubin.Static.Models;

namespace Rubin.Static.Rendering;

public interface IRenderPages
{
    Task<RenderedPage> IndexPage(IEnumerable<Post> posts);
    Task<RenderedPage> CategoryPage(Category category, IEnumerable<Post> posts);
    Task<IEnumerable<RenderedPage>> CategoryPage(Dictionary<Category, IEnumerable<Post>> posts);
    Task<IEnumerable<RenderedPage>> PostPage(IEnumerable<Post> posts);
    Task<RenderedPage> AllPage(IEnumerable<Post> posts);
}
