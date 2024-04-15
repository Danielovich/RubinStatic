using Rubin.Markdown.Extensions;
using Rubin.Markdown.Models;

namespace Rubin.Markdown.MarkdownDownload;
public interface IDownloadMarkdownFile
{
    Task<List<MardownFile>> DownloadAsync(IEnumerable<Uri> uris);
}

public class DownloadMarkdownFileService : IDownloadMarkdownFile
{
    private readonly HttpClient httpClient;

    public List<DownloadMarkdownExecption> DownloadExceptions { get; set; } = new();

    public DownloadMarkdownFileService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<MardownFile>> DownloadAsync(IEnumerable<Uri> uris)
    {
        var markdownDowloads = new List<MardownFile>();

        foreach (var uri in uris.ByValidFileExtensions(ValidMarkdownFileExtensions.ValidFileExtensions))
        {
            try
            {
                markdownDowloads.Add(
                    await DownloadAsync(uri)
                );
            }
            catch (HttpRequestException hre)
            {
                DownloadExceptions.Add(new DownloadMarkdownExecption($"{hre.Message}", hre));
            }
            catch (Exception e)
            {
                DownloadExceptions.Add(new DownloadMarkdownExecption($"{e.Message}", e));
            }
        }

        return markdownDowloads;
    }

    private async Task<MardownFile> DownloadAsync(Uri uri)
    {
        var result = await httpClient.GetAsync(uri);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Could not download file at {uri}", null, result.StatusCode);
        }

        MardownFile markdownFile = new();
        markdownFile.Contents = await result.Content.ReadAsStringAsync();

        return markdownFile;
    }
}
