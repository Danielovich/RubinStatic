namespace Rubin.Static.Console.Mappers;


/*
You might wonder, could this not be residing in the Rubin.Static namespace ?

No.

Because there are no project-references between either of those projects. And there shouldn't be.
*/
public class PostMapper : IMapToPosts
{
    private readonly ITransformMarkdownToHTML transformMarkdownToHTML;
    private readonly IParseMarkdownFilesToMarkdownPosts parseMarkdownFilesToMarkdownPosts;

    public PostMapper(
        ITransformMarkdownToHTML transformMarkdownToHTML,
        IParseMarkdownFilesToMarkdownPosts parseMarkdownFilesToMarkdownPosts)
    {
        this.transformMarkdownToHTML = transformMarkdownToHTML;
        this.parseMarkdownFilesToMarkdownPosts = parseMarkdownFilesToMarkdownPosts;
    }


    public async Task<IEnumerable<Post>> MapToPostsAsync(IEnumerable<MarkdownPost> markdownPosts)
    {
        ArgumentNullException.ThrowIfNull(markdownPosts, nameof(markdownPosts));

        var posts = new List<Post>();

        markdownPosts.ToList().ForEach(async (post) =>
        {
            var p = new Post(
                new Title(post.Title),
                await transformMarkdownToHTML.TransformAsync(post.Content),
                post.Categories.Select(category => new Category() { Title = category }),
                new Slug(post.Slug),
                post.PubDate,
                post.IsPublished
            );

            // filter out whatever we don't want to work with
            if (p.IsPublished)
            {
                posts.Add(p);
            }
        });

        return await Task.FromResult(posts);
    }

    public async Task<IEnumerable<Post>> MapToPostsAsync()
    {
        var markdownPosts = await parseMarkdownFilesToMarkdownPosts.ParseAsync();

        return await MapToPostsAsync(markdownPosts);
    }
}
