namespace Rubin.Markdown.Models;

public class MarkdownPost
{
    public virtual IList<string> Categories { get; } = new List<string>();
    public virtual IList<string> Tags { get; } = new List<string>();
    public virtual string Content { get; set; } = string.Empty;
    public virtual string Excerpt { get; set; } = string.Empty;
    public virtual bool IsPublished { get; set; } = false;
    public virtual DateTime LastModified { get; set; } = DateTime.UtcNow;
    public virtual DateTime PubDate { get; set; } = DateTime.UtcNow;
    public virtual string Slug { get; set; } = string.Empty;
    public virtual string Title { get; set; } = string.Empty;
}
