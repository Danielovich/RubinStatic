using Rubin.Static.Extensions;
using Rubin.Static.Models;
using Rubin.Static.Services;

namespace Rubin.Markdown.Console.Generators;

public class PageGenerator : IGeneratePages
{
    private readonly ICategoryPostOrganizer categoryPostOrganizer;
    private readonly IGenerateStatic generateStatic;
    private readonly ISavePage pageSaver;


    public PageGenerator(
        ICategoryPostOrganizer categoryPostOrganizer,
        IGenerateStatic generateStatic,
        ISavePage contentSaver)
    {
        this.categoryPostOrganizer = categoryPostOrganizer ?? throw new ArgumentNullException(nameof(categoryPostOrganizer));
        this.generateStatic = generateStatic ?? throw new ArgumentNullException(nameof(generateStatic));
        this.pageSaver = contentSaver;
    }

    public async Task GeneratePostPage(IEnumerable<Post> posts)
    {
        // generate page for each post 
        await generateStatic.PostPage(posts, pageSaver.Save);
    }

    public async Task GenerateIndexPage(IEnumerable<Post> posts, int showNumberOfPosts = 10)
    {
        // generate page for each post 
        await generateStatic.IndexPage(posts.OrderByPublishDate().Take(showNumberOfPosts), 
            pageSaver.Save);
    }

    public async Task GenerateCategoryPage(IEnumerable<Post> posts)
    {
        // generate page that outputs posts within a category.
        var categoryPosts = categoryPostOrganizer.GetCategoryPosts(posts);
        await generateStatic.CategoryPage(categoryPosts.OrderByPublishDate(),
            pageSaver.Save);
    }

    public async Task GenerateAllPage(IEnumerable<Post> posts)
    {
        // generate page that outputs all posts.
        await generateStatic.AllPage(posts.OrderByPublishDate(),
            pageSaver.Save);
    }

    public async Task GenerateNoCategoryPage(IEnumerable<Post> posts, string uncategorizedTitle)
    {
        // generate page that outputs posts which has no category.
        var noCategoryPosts = categoryPostOrganizer.GetNoCategoryPosts(posts);
        await generateStatic.CategoryPage(new Category()
        {
            Title = uncategorizedTitle
        }, noCategoryPosts.OrderByPublishDate(), pageSaver.Save);
    }
}
