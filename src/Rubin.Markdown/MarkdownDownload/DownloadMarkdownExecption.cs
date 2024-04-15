namespace Rubin.Markdown.MarkdownDownload;
public class DownloadMarkdownExecption : Exception
{
    public DownloadMarkdownExecption(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
