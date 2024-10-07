namespace Rubin.Markdown.Models;

public class MarkdownFile
{
    public MarkdownFile()
    { }

    public MarkdownFile(string contents)
    {
        Contents = contents;
    }


    public MarkdownFile(Uri uri)
    {
        Path = uri;
    }

    public string Contents { get; set; } = string.Empty;

    // empty Uri will cause exception, so I added a placeholder
    public Uri Path { get; private set; } = new Uri("https://you/do/not/want/this/to/show");
}
