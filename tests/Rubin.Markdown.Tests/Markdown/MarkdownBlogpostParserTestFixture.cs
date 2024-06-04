using Rubin.Markdown.Models;

namespace Rubin.Markdown.Tests;

public class MarkdownBlogpostParserTestFixture
{
    public MardownFile MarkdownFile { get; private set; } = new MardownFile();

    public MarkdownBlogpostParserTestFixture()
    {
        var markdownFile = Path.Combine(Environment.CurrentDirectory, "Assets", "post.md");

        this.MarkdownFile.Contents = File.ReadAllText(markdownFile);
    }
}
