namespace Rubin.Markdown.GithubClient
{
    //https://docs.github.com/en/rest/repos/contents?apiVersion=2022-11-28#about-repository-contents
    public class GetRepositoryContentApiResponse
    {
        [System.Text.Json.Serialization.JsonPropertyName("download_url")]
        public string DownloadUrl { get; set; } = string.Empty;
    }
}
