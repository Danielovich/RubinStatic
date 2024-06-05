using Rubin.Markdown.GithubClient;
using Rubin.Markdown.MarkdownDownload;
using Rubin.Markdown.Models;

namespace Rubin.Markdown.Tests;

public class MarkdownPostParserServiceTests
{
    [Theory, AutoMoqData]
    public async Task Parse_Markdown_To_MarkdownPost_ReturnsPosts(
        [Frozen] IGithubRepositoryContentsService githubContentsService,
        [Frozen] IDownloadMarkdownFile onlineContentService)
    {
        // Arrange
        var sut = new MarkdownPostParserService(githubContentsService, onlineContentService);

        // Act
        var result = await sut.ParseAsync(MarkdownFileTestHelper.MarkdownFileCollection());

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<MarkdownPost>>(result);
    }
}


