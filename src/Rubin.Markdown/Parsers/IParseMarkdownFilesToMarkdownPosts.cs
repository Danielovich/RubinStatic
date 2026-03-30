namespace Rubin.Markdown.Parsers;
using Rubin.Markdown.Models;

public interface IParseMarkdownFilesToMarkdownPosts
{
    Task<List<MarkdownPost>> ParseAsync();
    Task<List<MarkdownPost>> ParseAsync(List<MarkdownFile> markdownFiles);
}
