namespace Rubin.Markdown.GithubClient;

public interface IGithubRepositoryContentsService
{
    IReadOnlyList<GetRepositoryContentApiResponse> RepositoryContents { get; }

    Task LoadContentsAsync();
}
