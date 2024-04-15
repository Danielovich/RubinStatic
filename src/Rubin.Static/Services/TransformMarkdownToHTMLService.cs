using Rubin.Static.Models;

namespace Rubin.Static.Services;

public class TransformMarkdownToHTMLService : ITransformMarkdownToHTML
{
    public async Task<HtmlContent> TransformAsync(string markdown)
    {
        var content = new HtmlContent { Content = Markdig.Markdown.ToHtml(markdown) };

        return await Task.FromResult(content);
    }
}
