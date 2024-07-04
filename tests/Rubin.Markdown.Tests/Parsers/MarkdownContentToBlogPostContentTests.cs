namespace Rubin.Markdown.Tests.Parsers;
public class MarkdownContentToBlogPostContentTests : IClassFixture<MarkdownFileFixture>
{
    private readonly MarkdownFileFixture markdownFileFixture;

    public MarkdownContentToBlogPostContentTests(MarkdownFileFixture markdownFileFixture)
    {
        this.markdownFileFixture = markdownFileFixture;
    }

    [Fact]
    public async Task Parses_Markdown_Content_To_MarkdownPost_Content()
    {
        var markdownPostParser = new MarkdownPostParser(markdownFileFixture.MarkdownFile);
        await markdownPostParser.ParseContent();

        Assert.True(markdownPostParser.MarkdownPost.Content != string.Empty);
    }
}
