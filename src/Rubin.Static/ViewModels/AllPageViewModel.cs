using Rubin.Static.Models;

namespace Rubin.Static.ViewModels;
public class AllPageViewModel
{
    public IEnumerable<Post> Posts { get; set; }

    public AllPageViewModel(IEnumerable<Post> posts)
    {
        this.Posts = posts;
    }
}
