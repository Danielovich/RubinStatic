using Rubin.Markdown.Models;

namespace Rubin.Markdown.Tests;

public class MarkdownFileFixture
{
    public MardownFile MarkdownFile { get; private set; } = new MardownFile();

    public MarkdownFileFixture()
    {
        var markdownFile = Path.Combine(Environment.CurrentDirectory, "Assets", "post.md");

        this.MarkdownFile.Contents = File.ReadAllText(markdownFile);
    }
}
