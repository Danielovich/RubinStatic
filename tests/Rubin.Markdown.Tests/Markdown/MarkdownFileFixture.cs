using Rubin.Markdown.Models;

namespace Rubin.Markdown.Tests;

public class MarkdownFileFixture
{
    public MarkdownFile MarkdownFile { get; private set; } = new MarkdownFile();

    public MarkdownFileFixture()
    {
        var markdownFile = Path.Combine(Environment.CurrentDirectory, "Assets", "post.md");

        this.MarkdownFile.Contents = File.ReadAllText(markdownFile);
    }
}
