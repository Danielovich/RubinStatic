namespace Rubin.Markdown.Tests.GithubClient;

internal class FakeGithubApi
{
    public HttpResponseMessage ContentsEndpoint()
    {
        // Return response from https://api.github.com/repos/Danielovich/danielovich.github.io/contents/_posts?ref=master
        var jsonResponseTestPath = Path.Combine(Environment.CurrentDirectory, "Assets", "contentsresponse.json");
        var jsonResponse = File.ReadAllText(jsonResponseTestPath);

        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json")
        };

        return response;
    }
}
