namespace Rubin.Static.Models;

public class Post
{
    public Title Title { get; set; } = new Title();
    public HtmlContent HtmlContent { get; set; } = new HtmlContent();
    public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    public Slug Slug { get; set; }
    public DateTime PublishedDate { get; set; }
    public bool IsPublished { get; set; }

    public Post(Title postTitle, HtmlContent htmlContent, IEnumerable<Category> categories, Slug slug, DateTime publishedDate, bool isPublished)
    {
        Title = postTitle;
        HtmlContent = htmlContent;
        Categories = categories;
        Slug = slug;
        PublishedDate = publishedDate;
        IsPublished = isPublished;
    }
}
