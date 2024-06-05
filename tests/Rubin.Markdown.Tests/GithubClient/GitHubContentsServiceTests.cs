using Rubin.Markdown.GithubClient;

namespace Rubin.Markdown.Tests.GithubClient;
public class GitHubContentsServiceTests : IDisposable
{
    public void Dispose()
    {
        HttpClient.Dispose();
    }
    private Mock<HttpMessageHandler> HttpMessageHandlerMock { get; set; }
    private HttpClient HttpClient { get; set; }
    private IConfiguration Configuration { get; set; }

    public GitHubContentsServiceTests()
    {
        HttpMessageHandlerMock = new Mock<HttpMessageHandler>();
        HttpClient = new HttpClient(HttpMessageHandlerMock.Object);

        var inMemorySettings = new Dictionary<string, string?> {
            {"markdownContentsUrl", "https://any.uri.will.do"},
        };

        Configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
    }

    [Fact]
    public void GithubPosts_Are_Available()
    {
        // Arrange
        var gitHubResponse = new FakeGithubApi();

        HttpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(gitHubResponse.ContentsEndpoint);

        var sut = new GitHubApiService(HttpClient, Configuration);

        // Act
        var posts = sut.LoadContentsAsync();

        // Assert
        Assert.True(sut.RepositoryContents.Count > 0);
    }
}
