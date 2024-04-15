using Rubin.Markdown.GithubClient;

namespace Rubin.Markdown.Tests.GithubClient;
public class GitHubContentsServiceTests : IDisposable
{
    public void Dispose()
    {
        HttpClient.Dispose();
    }
    private Mock<HttpMessageHandler> HttpMessageHandlerMock { get; set; } = default!;
    private HttpClient HttpClient { get; set; } = default!;
    private IConfiguration Configuration { get; set; } = default!;

    public GitHubContentsServiceTests()
    {
        HttpMessageHandlerMock = new Mock<HttpMessageHandler>();
        HttpClient = new HttpClient(HttpMessageHandlerMock.Object);

        ///Be aware here that this must correspond to the settings inside the <see cref="Constants" /> file.
        var inMemorySettings = new Dictionary<string, string?> {
            {"markdownContentsUrl", "https://api.github.com/repos/Danielovich/danielovich.github.io/contents/_posts?ref=master"},
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
