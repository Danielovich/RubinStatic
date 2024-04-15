namespace Rubin.Markdown.Models
{
    // Represents a full markdown comment.
    // Such as [//]: # "categories: ugga, bugga, johnny"
    public class MarkdownComment
    {
        public MarkdownComment(string comment)
        {
            Comment = comment;
        }

        public string Comment { get; set; } = default!;
    }
}
