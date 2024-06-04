namespace Rubin.Static.Tests;
public class MarkdownReader
{
    public string Markdown { get; private set; } = string.Empty;

    public MarkdownReader()
    {
        var markdownFile = Path.Combine(Environment.CurrentDirectory, "Assets", "markdown.md");

        this.Markdown = File.ReadAllText(markdownFile);
    }
}
