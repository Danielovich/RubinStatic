namespace Rubin.Markdown.Tests.GithubClient;

internal class FakeGithubApi
{
    public HttpResponseMessage ContentsEndpoint()
    {
        var githubContentsResponseJsonPath = Path.Combine(Environment.CurrentDirectory, "Assets", "contentsresponse.json");
        var jsonResponse = File.ReadAllText(githubContentsResponseJsonPath);

        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json")
        };

        return response;
    }
}
