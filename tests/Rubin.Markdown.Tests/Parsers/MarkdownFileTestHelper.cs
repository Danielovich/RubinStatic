using Rubin.Markdown.Models;

namespace Rubin.Markdown.Tests;

public static class MarkdownFileTestHelper
{
    public static List<MardownFile> MarkdownFileCollection()
    {
        var markdownFile = Path.Combine(Environment.CurrentDirectory, "assets", "post.md");

        List<MardownFile> markdownFiles = new();
        markdownFiles.Add(new MardownFile() { Contents = File.ReadAllText(markdownFile) });

        return markdownFiles;
    }
}


