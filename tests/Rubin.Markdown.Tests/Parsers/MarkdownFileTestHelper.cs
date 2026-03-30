using Rubin.Markdown.Models;

namespace Rubin.Markdown.Tests;

public static class MarkdownFileTestHelper
{
    public static List<MarkdownFile> MarkdownFileCollection()
    {
        var markdownFile = Path.Combine(Environment.CurrentDirectory, "Assets", "post.md");

        List<MarkdownFile> markdownFiles = new();
        markdownFiles.Add(new MarkdownFile() { Contents = File.ReadAllText(markdownFile) });

        return markdownFiles;
    }
}


