using Rubin.Markdown.Extensions;
using Rubin.Markdown.MarkdownDownload;

namespace Rubin.Markdown.Tests;

public class OnlineContentServiceTests : IDisposable
{
    public void Dispose()
    {
        HttpClient.Dispose();
    }
    private Mock<HttpMessageHandler> HttpMessageHandlerMock { get; set; }
    private HttpClient HttpClient { get; set; }

    public OnlineContentServiceTests()
    {
        HttpMessageHandlerMock = new Mock<HttpMessageHandler>();
        HttpClient = new HttpClient(HttpMessageHandlerMock.Object);
    }

    private async Task ArrangeResponse(Func<HttpResponseMessage> response)
    {
        HttpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        await Task.CompletedTask;
    }

    private HttpResponseMessage NewHttpResponseMessage(HttpStatusCode httpStatusCode = HttpStatusCode.OK)
    {
        return new HttpResponseMessage()
        {
            StatusCode = httpStatusCode
        };
    }

    [Theory]
    [AutoData]
    public async Task On_UnSuccessful_Status_Code_Errors_Are_Present(List<Uri> listOfUris)
    {
        // Arrange

        //We add a valid file extension path
        //otherwise we do not enter actual method because it filters out non markdown files
        listOfUris.Add(new Uri("https://some.dk/domain/file.md")); 

        await ArrangeResponse(() => NewHttpResponseMessage(HttpStatusCode.Gone));

        var sut = new DownloadMarkdownFileService(this.HttpClient);

        // Act
        var result = await sut.DownloadAsync(listOfUris);

        // Assert
        Assert.True(sut.DownloadExceptions.Any());
    }

    [Theory]
    [MarkdownUriAutoData]
    public async Task Posts_Are_Downloaded(List<Uri> listOfUris)
    {
        // Arrange
        await ArrangeResponse(() => NewHttpResponseMessage());

        var sut = new DownloadMarkdownFileService(this.HttpClient);

        //Act
        var contents = await sut.DownloadAsync(listOfUris);

        // Assert
        this.HttpMessageHandlerMock
            .Protected()
            .Verify("SendAsync",
                Times.Exactly(3),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());

        Assert.True(contents.Count == 3);
    }

    [Theory]
    [MarkdownUriAutoData]
    public async Task Only_Markdown_Uris_Are_Valid(List<Uri> listOfUris)
    {
        //Arrange 

        //add some invalid Uris
        listOfUris.Add(new Uri("https://some.dk/domain/file"));
        listOfUris.Add(new Uri("https://some.dk/domain/file.jpg"));

        await ArrangeResponse(() => NewHttpResponseMessage());

        var sut = new DownloadMarkdownFileService(this.HttpClient);

        var contents = await sut.DownloadAsync(listOfUris);

        this.HttpMessageHandlerMock
            .Protected()
            .Verify("SendAsync",
                Times.Exactly(3),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());

        Assert.True(contents.Count == 3);
    }
}
