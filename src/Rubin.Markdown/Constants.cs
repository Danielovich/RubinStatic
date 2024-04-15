namespace Rubin.Markdown;

public static class Constants
{
    public static class Config
    {
        public static readonly string DateTimeFormat1 = "dd/MM/yyyy HH:mm";
        public static readonly string DateTimeFormat2 = "d/MM/yyyy HH:mm";
        public static readonly string DateTimeFormat3 = "dd/M/yyyy HH:mm";
        public static readonly string DateTimeFormat4 = "d/M/yyyy HH:mm";
        public static readonly string GithubClientUserAgent = "Markdown.GithubClient";

        //this is set in an appsettings.json, with the key name equaling "markdownContentsUrl"
        public static readonly string MarkdownContentsUrl = "markdownContentsUrl";
    }
}

