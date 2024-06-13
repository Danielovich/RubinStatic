namespace Rubin.Static.Tests;

public class PageRenderingTests : IClassFixture<PredictablePostCategoriesTestsFixture>
{
    private readonly Fixture _fixture;

    public PageRenderingTests(PredictablePostCategoriesTestsFixture postPredictableCategoriesTestsFixture)
    {
        this._fixture = postPredictableCategoriesTestsFixture.Fixture;
    }

    [Theory]
    [RenderData]
    public async Task Render_Category_Page(PageRendering render)
    {
        // Arrange
        var listOfPosts = _fixture.CreateMany<Post>(10).ToList();
        var allCategories = listOfPosts.SelectMany(p => p.Categories).ToList();

        CategoryPostsService categoryPostsService = new();
        var posts = categoryPostsService.GetPostsInCategory(allCategories[0], listOfPosts);

        // Act
        var result = await render.CategoryPage(allCategories[0], posts);

        // Assert
        Assert.True(result.Slug.Instance.Length > 0);
        Assert.True(result.Content.Length > 0);
    }

    [Theory]
    [RenderData]
    public async Task Render_Post_Page(PageRendering render)
    {
        // Arrange
        var listOfPosts = _fixture.CreateMany<Post>(2).ToList();

        // Act
        var result = await render.PostPage(listOfPosts);

        // Assert
        Assert.True(result.Count() == 2);
    }
}
