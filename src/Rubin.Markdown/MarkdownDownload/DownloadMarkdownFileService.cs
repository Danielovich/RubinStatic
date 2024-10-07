using Rubin.Markdown.Models;

namespace Rubin.Markdown.MarkdownDownload;
public interface IDownloadMarkdownFile
{
    Task<List<MarkdownFile>> DownloadAsync(IEnumerable<Uri> uris);
}

public class DownloadMarkdownFileService : IDownloadMarkdownFile
{
    private readonly string[] ValidFileExtensions = { ".md", ".markdown" };

    private readonly HttpClient httpClient;

    public List<DownloadMarkdownExecption> DownloadExceptions { get; private set; } = new();

    public DownloadMarkdownFileService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<MarkdownFile>> DownloadAsync(IEnumerable<Uri> uris)
    {
        var markdownDowloads = new List<MarkdownFile>();

        foreach (var markdownFile in ValidMarkdownUris(uris))
        {
            try
            {
                markdownDowloads.Add(
                    await DownloadAsync(markdownFile)
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

    private async Task<MarkdownFile> DownloadAsync(MarkdownFile markdownFile)
    {
        var result = await httpClient.GetAsync(markdownFile.Path);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Could not download file at {markdownFile.Path}", null, result.StatusCode);
        }

        markdownFile.Contents = await result.Content.ReadAsStringAsync();

        return markdownFile;
    }

    private IEnumerable<MarkdownFile> ValidMarkdownUris(IEnumerable<Uri> uris)
    {
        foreach (var uri in uris)
        {
            if (ValidFileExtensions.Contains(UriPathExtension(uri)))
            {
                yield return new MarkdownFile(uri);
            }
        }
    }

    private string UriPathExtension(Uri uri)
    {
        if (uri == null) return string.Empty;

        var fileName = Path.GetFileName(uri.AbsoluteUri);

        if (fileName.Contains('.'))
        {
            return Path.GetExtension(fileName);
        }

        return string.Empty;
    }
}