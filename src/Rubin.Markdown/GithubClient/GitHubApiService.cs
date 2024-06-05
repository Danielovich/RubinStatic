namespace Rubin.Markdown.GithubClient;

public class GitHubApiService : IGithubRepositoryContentsService
{
    private readonly HttpClient httpClient;
    private readonly IConfiguration configuration;

    public IReadOnlyList<GetRepositoryContentApiResponse> RepositoryContents { get; private set; }

    public GitHubApiService(HttpClient httpClient, IConfiguration config)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.configuration = config ?? throw new ArgumentNullException(nameof(config));

        RepositoryContents = new List<GetRepositoryContentApiResponse>();

        ArgumentException.ThrowIfNullOrWhiteSpace(configuration[Constants.Config.MarkdownContentsUrl]);

        // github will not allow any requests without a user agent
        this.httpClient.DefaultRequestHeaders.Add("User-Agent", Constants.Config.GithubClientUserAgent);
    }

    public async Task LoadContentsAsync()
    {
        var response = await httpClient.GetAsync(configuration[Constants.Config.MarkdownContentsUrl]);
        var json = await response.Content.ReadAsStringAsync();

        var apiResponse = JsonSerializer.Deserialize<List<GetRepositoryContentApiResponse>>(json) 
            ?? new List<GetRepositoryContentApiResponse>();

        RepositoryContents = apiResponse;
    }
}
