namespace Rubin.Markdown.Parsers;

using Rubin.Markdown.GithubClient;
using Rubin.Markdown.MarkdownDownload;
using Rubin.Markdown.Models;

public interface IParseMarkdownFilesToMarkdownPosts
{
    Task<List<MarkdownPost>> ParseAsync();
    Task<List<MarkdownPost>> ParseAsync(List<MardownFile> markdownFiles);
}

public class MarkdownPostParserService : IParseMarkdownFilesToMarkdownPosts
{
    private readonly IGithubRepositoryContentsService githubContentsService;
    private readonly IDownloadMarkdownFile markdownDownloadService;
    public MarkdownPostParserService(IGithubRepositoryContentsService githubContentsService,
        IDownloadMarkdownFile markdownDownloadService)
    {
        this.markdownDownloadService = markdownDownloadService;
        this.githubContentsService = githubContentsService;
    }

    /// <remarks>This will initiate an external resource call over http</remarks>
    public async Task<List<MarkdownPost>> ParseAsync()
    {
        await githubContentsService.LoadContentsAsync();

        var contents = await markdownDownloadService.DownloadAsync(
            githubContentsService.RepositoryContents.Select(d => new Uri(d.DownloadUrl)));

        return await ParseAsync(contents);
    }

    public async Task<List<MarkdownPost>> ParseAsync(List<MardownFile> markdownFiles)
    {
        var intermediatePosts = new List<MarkdownPost>();

        foreach (var item in markdownFiles)
        {
            var parser = new MarkdownPostParser(item);

            await parser.ParseCommentsAsPropertiesAsync();
            await parser.ParseContent();

            intermediatePosts.Add(parser.MarkdownPost);
        }

        return intermediatePosts;
    }
}
