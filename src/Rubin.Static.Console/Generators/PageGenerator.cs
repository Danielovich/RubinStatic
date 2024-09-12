namespace Rubin.Static.Console.Generators;

public class PageGenerator : IGeneratePages
{
    private readonly ICategoryPostOrganizer categoryPostOrganizer;
    private readonly IRenderPages generateStatic;
    private readonly ISaveRenderedPage pageSaver;


    public PageGenerator(
        ICategoryPostOrganizer categoryPostOrganizer,
        IRenderPages generateStatic,
        ISaveRenderedPage contentSaver)
    {
        this.categoryPostOrganizer = categoryPostOrganizer ?? throw new ArgumentNullException(nameof(categoryPostOrganizer));
        this.generateStatic = generateStatic ?? throw new ArgumentNullException(nameof(generateStatic));
        this.pageSaver = contentSaver;
    }

    public async Task GeneratePostPage(IEnumerable<Post> posts)
    {
        // generate page for each post 
        var postPage = await generateStatic.PostPage(posts);
        await pageSaver.Save(postPage);
    }

    public async Task GenerateIndexPage(IEnumerable<Post> posts, int showNumberOfPosts = 10)
    {
        // generate page for each post 
        var index = await generateStatic.IndexPage(posts.OrderByPublishDate().Take(showNumberOfPosts));
        await pageSaver.Save(index);
    }

    public async Task GenerateCategoryPage(IEnumerable<Post> posts)
    {
        // generate category page and belonging posts within that category.
        var categoryPosts = categoryPostOrganizer.GetCategoryPosts(posts);
        var categoryPage = await generateStatic.CategoryPage(categoryPosts.OrderByPublishDate());
        await pageSaver.Save(categoryPage);

    }

    public async Task GenerateAllPage(IEnumerable<Post> posts)
    {
        // generate page that outputs all posts.
        var allPage = await generateStatic.AllPage(posts.OrderByPublishDate());
        await pageSaver.Save(allPage);
    }

    public async Task GenerateNoCategoryPage(IEnumerable<Post> posts, string uncategorizedTitle)
    {
        // generate page that outputs posts which has no category.
        var noCategoryPosts = categoryPostOrganizer.GetNoCategoryPosts(posts);
        var categoryPage = await generateStatic.CategoryPage(new Category()
        {
            Title = uncategorizedTitle
        }, noCategoryPosts.OrderByPublishDate());

        await pageSaver.Save(categoryPage);
    }
}
