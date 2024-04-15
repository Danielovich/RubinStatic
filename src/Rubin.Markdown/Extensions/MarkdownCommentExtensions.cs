using Rubin.Markdown.Models;

namespace Rubin.Markdown.Extensions;

public static class MarkdownCommentExtensions
{
    public static string GetValueBetweenFirstQuoteAndColon(this MarkdownComment comment)
    {
        // Regex pattern to find the value between the first quote and the first colon
        string pattern = "\"(.*?):";

        Match match = Regex.Match(comment.Comment, pattern);
        if (match.Success)
        {
            return match.Groups[1].Value.Trim();
        }

        return string.Empty;
    }


    public static MarkdownProperty AsPostProperty(this MarkdownComment commentAsProperty, string commentProperty)
    {
        // it exercises on a comment like this [//]: # "title: hugga bugga ulla johnson"
        var pattern = $"{commentProperty}:\\s*(.*?)\"";

        Match match = Regex.Match(commentAsProperty.Comment, pattern);
        if (match.Success)
        {
            return new MarkdownProperty { Value = match.Groups[1].Value };
        }

        return new MarkdownProperty();
    }

    public static IEnumerable<MarkdownProperty> ToProperty(this MarkdownComment commentAsProperty, string commentProperty, char delimeter)
    {
        // it exercises on a comment that reflects a list of values,
        // e.g [//]: # "categories: ugga, bugga, johnny"
        var pattern = $"{commentProperty}:\\s*(.*?)\"";

        Match match = Regex.Match(commentAsProperty.Comment, pattern);
        if (match.Success)
        {
            var listOfValues = match.Groups[1].Value.Split(delimeter);

            foreach (var value in listOfValues)
            {
                yield return new MarkdownProperty { Value = value };
            }
        }
    }
}
