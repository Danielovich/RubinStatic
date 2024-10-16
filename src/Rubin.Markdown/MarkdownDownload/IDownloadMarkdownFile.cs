using Rubin.Markdown.Models;

namespace Rubin.Markdown.MarkdownDownload;

public interface IDownloadMarkdownFile
{
    Task<List<MarkdownFile>> DownloadAsync(IEnumerable<Uri> uris, CancellationToken cancellationToken = default);
}
