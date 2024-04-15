namespace Rubin.Markdown.Models;

public class MardownFile
{
    public MardownFile()
    {
        
    }

    public MardownFile(string contents)
    {
        Contents = contents;
    }

    public string Contents { get; set; } = string.Empty;
}
