namespace Rubin.Static.Models;
public class Category
{
    public string Title { get; set; } = string.Empty;
    public Slug Slug { get { return new Slug(Title); } }
}
