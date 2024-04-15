using Rubin.Static.Models;

namespace Rubin.Static.Services;

public interface ITransformMarkdownToHTML
{
    Task<HtmlContent> TransformAsync(string markdown);
}
