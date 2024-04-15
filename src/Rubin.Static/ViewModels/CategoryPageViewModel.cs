using Rubin.Static.Extensions;
using Rubin.Static.Models;

namespace Rubin.Static.ViewModels;

public class CategoryPageViewModel
{
    public Category Category { get; internal set; }
    public string Reference { get; internal set; }
    public IEnumerable<Post> Posts { get; set; }

    public CategoryPageViewModel(Category category, IEnumerable<Post> posts)
    {
        Category = category;
        Posts = posts;
        Reference = category.Slug.ToUri();
    }
}
