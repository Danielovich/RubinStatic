using Rubin.Static.Models;

namespace Rubin.Static.ViewModels;
public class IndexPageViewModel
{
    public IEnumerable<Post> Posts { get; set; }

    public IndexPageViewModel(IEnumerable<Post> posts)
    {
        this.Posts = posts;
    }
}
