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
        // we want to turn a MarkdownComment, e.g. [//]: # "title: hugga bugga ulla johnson"
        // into a property
        var pattern = $"{commentProperty}:\\s*(.*?)\"";

        Match match = Regex.Match(commentAsProperty.Comment, pattern);
        if (match.Success)
        {
            return new MarkdownProperty { Value = match.Groups[1].Value };
        }

        return new MarkdownProperty();
    }

    public static IEnumerable<MarkdownProperty> ToPostProperty(this MarkdownComment commentAsProperty, string commentProperty, char delimeter)
    {
        // we want to turn a MarkdownComment, e.g [//]: # "categories: ugga, bugga, johnny"
        // into enumerator values
        var pattern = $"{commentProperty}:\\s*(\\S.*?)\"";

        Match match = Regex.Match(commentAsProperty.Comment, pattern, RegexOptions.IgnorePatternWhitespace);
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
