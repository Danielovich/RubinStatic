namespace Rubin.Static.Tests;

public class TransformMarkdownToHTMLTests
{
    [Theory]
    [AutoData]
    public async Task Markdown_is_transformed_into_html(MarkdownReader markdownReader)
    {
        TransformMarkdownToHTMLService transformMarkdownToHTML = new();

        var html = await transformMarkdownToHTML.TransformAsync(markdownReader.Markdown);

        // poor mans check
        Assert.True(html.Content.IndexOf("<h3>") != -1);
        Assert.True(html.Content.IndexOf("<p>") != -1);
        Assert.True(html.Content.IndexOf("<h1>") != -1);
    }
}