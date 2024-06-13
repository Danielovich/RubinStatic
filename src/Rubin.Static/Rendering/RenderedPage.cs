namespace Rubin.Static.Rendering;

public class RenderedPage
{
    public RenderedPage(Slug slug, string content)
    {
        this.Slug = slug;
        this.Content = content;
    }

    public Slug Slug { get; set; }
    public string Content { get; set; }
}
