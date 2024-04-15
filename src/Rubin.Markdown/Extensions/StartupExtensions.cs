using Rubin.Markdown.GithubClient;
using Rubin.Markdown.MarkdownDownload;
using Rubin.Markdown.Parsers;

namespace Rubin.Markdown.Extensions;

public static class StartupExtensions
{
    public static IServiceCollection AddMarkdown(this IServiceCollection services)
    {
        services.AddHttpClient<GitHubApiService>();
        services.AddHttpClient<DownloadMarkdownFileService>();
        services.AddSingleton<IParseMarkdownFilesToMarkdownPosts, MarkdownPostParserService>();
        services.AddSingleton<IDownloadMarkdownFile, DownloadMarkdownFileService>();
        services.AddSingleton<IGithubRepositoryContentsService, GitHubApiService>();

        return services;
    }
}
