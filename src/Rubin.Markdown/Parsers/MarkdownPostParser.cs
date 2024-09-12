using Rubin.Markdown.Extensions;
using Rubin.Markdown.Models;

namespace Rubin.Markdown.Parsers;

public class MarkdownPostParser
{
    private const string validMarkdownComment1 = "[//]: #";
    private const string validMarkdownComment2 = "[//]:#";

    public string Markdown { get; set; } = string.Empty;
    public MarkdownPost MarkdownPost { get; private set; }


    public MarkdownPostParser(MardownFile markdownFile)
    {
        ArgumentNullException.ThrowIfNull(markdownFile.Contents);

        Markdown = markdownFile.Contents;
        MarkdownPost = new MarkdownPost();
    }

    /// <summary>
    /// Parses comments as markdown post properties
    /// </summary>
    public async Task ParseCommentsAsPropertiesAsync()
    {
        using var reader = new StringReader(Markdown);

        // line of content from the Markdown
        string? line;

        // Convention is that all comments from top of the MarkDown is a future
        // post property. As soon as the parser sees something else it breaks.
        while ((line = reader.ReadLine()) != null)
        {
            if (!(line.StartsWith(validMarkdownComment1) || line.StartsWith(validMarkdownComment2)))
            {
                break;
            }

            if (line.StartsWith(validMarkdownComment1) || line.StartsWith(validMarkdownComment2))
            {
                var markdownComment = new MarkdownComment(line);

                // somewhat a tedious bit of code but it's easy to understand
                // we start by looking at what type of comment we have on our hands
                // from there we extract the comment value and turn that into a property on a markdown post
                switch (markdownComment.GetValueBetweenFirstQuoteAndColon())
                {
                    case "title":
                        MarkdownPost.Title = markdownComment.AsPostProperty("title").Value;
                        break;
                    case "slug":
                        MarkdownPost.Slug = markdownComment.AsPostProperty("slug").Value;
                        break;
                    case "pubDate":
                        MarkdownPost.PubDate = markdownComment.AsPostProperty("pubDate").ParseToDate();
                        break;
                    case "lastModified":
                        MarkdownPost.LastModified = markdownComment.AsPostProperty("lastModified").ParseToDate();
                        break;
                    case "excerpt":
                        MarkdownPost.Excerpt = markdownComment.AsPostProperty("excerpt").Value;
                        break;
                    case "categories":
                        markdownComment.ToPostProperty("categories", ',')
                            .Select(n => n.Value)
                            .ToList()
                            .ForEach(MarkdownPost.Categories.Add);
                        break;
                    case "isPublished":
                        MarkdownPost.IsPublished = markdownComment.AsPostProperty("isPublished").ParseToBool();
                        break;
                    default:
                        break;
                }
            }
        }

        await Task.CompletedTask;
    }

    /// <summary>
    /// Reads all content which are not markdown properties. 
    /// </summary>
    public async Task ParseContent()
    {
        using var reader = new StringReader(Markdown);
        using var contenWriter = new StringWriter();

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            // we found a property, continue looping
            if (line.StartsWith(validMarkdownComment1) || line.StartsWith(validMarkdownComment2))
            {
                continue;
            }

            if (!line.StartsWith(validMarkdownComment1) || line.StartsWith(validMarkdownComment2))
            {
                contenWriter.WriteLine(line);
            }
        }

        MarkdownPost.Content = contenWriter.ToString();

        await Task.CompletedTask;
    }
}
