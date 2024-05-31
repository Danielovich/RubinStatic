namespace Rubin.Static.Tests;

public class CategoryPostsTests : IClassFixture<PredictablePostCategoriesTestsFixture>
{
    private readonly Fixture _fixture;

    public CategoryPostsTests(PredictablePostCategoriesTestsFixture fixture)
    {
        _fixture = fixture.Fixture;
    }

    [Fact]
    public void Only_Distinct_Categories_Is_Returned()
    {
        // Arrange
        var listOfPosts = _fixture.CreateMany<Post>(10);
           
        var sut = new CategoryPostsService();

        // Act
        var result = sut.GetCategoryPosts(listOfPosts);

        // Assert
        // 5 is the number of categories we set up in the fixture
        Assert.True(result.Keys.Count == 5); 
    }

    [Fact]
    public void Posts_Keep_Its_Categories()
    {
        // Arrange
        var listOfPosts = _fixture.CreateMany<Post>(10).ToList();
        
        var firstPost = listOfPosts.First();

        // add posts with same categories as first post in collection
        var post1 = _fixture.Create<Post>();
        post1.Categories = firstPost.Categories;
        listOfPosts.Add(post1);

        var post2 = _fixture.Create<Post>();
        post2.Categories = firstPost.Categories;
        listOfPosts.Add(post2);

        // Act
        var sut = new CategoryPostsService();
        var result = sut.GetCategoryPosts(listOfPosts);

        // Assert
        // if post1 and post2 share the same categories as first post we are all good.
        var posts1 = result.Values.SelectMany(s => s).Where(p => p.Title.Equals(post1.Title)).First();
        Assert.True(firstPost.Categories.SequenceEqual(posts1.Categories));

        var posts2 = result.Values.SelectMany(s => s).Where(p => p.Title.Equals(post2.Title)).First();
        Assert.True(firstPost.Categories.SequenceEqual(posts2.Categories));
    }

    [Fact]
    public void Posts_With_No_Category_Is_Not_Returned()
    {
        // Arrange
        var listOfPosts = _fixture.CreateMany<Post>(10).ToList();

        var firstPost = listOfPosts.First();
        
        // add a post
        var post = _fixture.Create<Post>();
        //no categories!
        post.Categories = new List<Category>();
        listOfPosts.Add(post);

        // Act
        var sut = new CategoryPostsService();
        var result = sut.GetCategoryPosts(listOfPosts);

        // Assert
        Assert.Empty(result.Values.SelectMany(s => s).Where(p => !p.Categories.Any()));
    }

    [Fact]
    public void Posts_With_No_Category_Is_Returned()
    {
        // Arrange
        var listOfPosts = _fixture.CreateMany<Post>(10).ToList();

        listOfPosts.First().Categories = new List<Category>();
        listOfPosts.First().Title.PostTitle = "Empty categories";

        // Act
        var sut = new CategoryPostsService();
        var result = sut.GetNoCategoryPosts(listOfPosts);

        // Assert
        Assert.True(result.Where(p => p.Title.PostTitle == "Empty categories").Any());
        Assert.True(result.Count() == 1);
    }
}
