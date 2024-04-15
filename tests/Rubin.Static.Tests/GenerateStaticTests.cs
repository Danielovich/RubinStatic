using AutoFixture;
using Rubin.Static.Models;
using Rubin.Static.Services;

namespace Rubin.Static.Tests
{
    public class GenerateStaticTests : IClassFixture<PostPredictableCategoriesTestsFixture>
    {
        private readonly Fixture _fixture;

        public GenerateStaticTests(PostPredictableCategoriesTestsFixture postPredictableCategoriesTestsFixture)
        {
            this._fixture = postPredictableCategoriesTestsFixture.Fixture;
        }

        [Theory]
        [RenderData]
        public async Task Render_Category_Page(GenerateStaticService gs)
        {
            // Arrange
            var listOfPosts = _fixture.CreateMany<Post>(10).ToList();
            var allCategories = listOfPosts.SelectMany(p => p.Categories).ToList();

            CategoryPostsService categoryPostsService = new();
            var posts = categoryPostsService.GetPostsInCategory(allCategories[0], listOfPosts);

            // Act
            var result = await gs.CategoryPage(allCategories[0], posts, SaveOutputAsync);

            // Assert
            Assert.True(result.Length > 0);
        }

        [Theory]
        [RenderData]
        public async Task Render_Post_Page(GenerateStaticService gs)
        {
            // Arrange
            var listOfPosts = _fixture.CreateMany<Post>(2).ToList();

            // Act
            var result = await gs.PostPage(listOfPosts, SaveOutputAsync);

            // Assert
            Assert.True(result.Length > 0);
        }

        private async Task<string> SaveOutputAsync(string input, Slug identifier)
        {
            var path = string.Concat(
                AppDomain.CurrentDomain.BaseDirectory,
                "Views",
                Path.DirectorySeparatorChar,
                identifier.Instance,
                ".html");

            await File.WriteAllTextAsync(path, input);

            return path;
        }
    }
}
