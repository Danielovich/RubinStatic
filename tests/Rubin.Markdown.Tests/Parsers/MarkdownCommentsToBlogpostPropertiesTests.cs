namespace Rubin.Markdown.Tests.Parsers;

public class MarkdownCommentsToBlogpostPropertiesTests
{
    [Fact]
    public async Task Parse_Comments_As_Blog_Properties()
    {
        var comments =
              "[//]: # \"title: hugga bugga ulla johnson\"\n" +
              "[//]: # \"johnny: hugga bugga ulla johnson\"";

        var markDownBlogpostParser = new MarkdownPostParser(new Models.MardownFile(comments));
        await markDownBlogpostParser.ParseCommentsAsPropertiesAsync();

        Assert.Equal("hugga bugga ulla johnson", markDownBlogpostParser.MarkdownPost.Title);
    }

    [Fact]
    public async Task Parse_All_Comments_To_Post_Properties()
    {
        var comments =
              "[//]: # \"title: hugga bugga ulla johnson\" \n" +
              "[//]: # \"slug: hulla bulla\" \n" +
              "[//]: # \"pubDate: 13/10/2017 18:59\"\n" +
              "[//]: # \"lastModified: 13/10/2017 23:59\"\n" +
              "[//]: # \"excerpt: an excerpt you would never imagine \"\n" +
              "[//]: # \"categories: cars, coding, personal, recipes \"\n" +
              "[//]: # \"isPublished: true \"";

        var markDownBlogpostParser = new MarkdownPostParser(new Models.MardownFile(comments));
        await markDownBlogpostParser.ParseCommentsAsPropertiesAsync();

        Assert.True(markDownBlogpostParser.MarkdownPost.Title != string.Empty);
        Assert.True(markDownBlogpostParser.MarkdownPost.Slug != string.Empty);
        Assert.True(markDownBlogpostParser.MarkdownPost.PubDate > DateTime.MinValue);
        Assert.True(markDownBlogpostParser.MarkdownPost.LastModified > DateTime.MinValue);
        Assert.True(markDownBlogpostParser.MarkdownPost.Excerpt != string.Empty);
        Assert.True(markDownBlogpostParser.MarkdownPost.Categories.Count() == 4);
        Assert.True(markDownBlogpostParser.MarkdownPost.IsPublished);
    }

    [Fact]
    public async Task Parse_Different_Comment_Format_As_Blog_Properties()
    {
        var comments =
              "[//]: #   \"title: hugga bugga ulla johnson\" \n" +
              "[//]:#\"slug: hulla bulla\" \n " +
              "[//]:# \"slug: hulla bulla\"";

        var markDownBlogpostParser = new MarkdownPostParser(new Models.MardownFile(comments));
        await markDownBlogpostParser.ParseCommentsAsPropertiesAsync();

        Assert.True(markDownBlogpostParser.MarkdownPost.Title != string.Empty);
        Assert.True(markDownBlogpostParser.MarkdownPost.Slug != string.Empty);
    }

    [Fact]
    public async Task Parse_Comments_Fails()
    {
        var comments =
              "[//]:   #\"title: hugga bugga ulla johnson\" \n" +
              "[//]:  # \"slug: hulla bulla\" \n";

        var markDownBlogpostParser = new MarkdownPostParser(new Models.MardownFile(comments));
        await markDownBlogpostParser.ParseCommentsAsPropertiesAsync();

        Assert.True(markDownBlogpostParser.MarkdownPost.Title == string.Empty);
        Assert.True(markDownBlogpostParser.MarkdownPost.Slug == string.Empty);
    }

    [Fact]
    public async Task Parse_Comments_As_Dates_Fails()
    {
        var comments =
              "[//]: # \"pubDate: 20171/13/10 18:59\"\n" +
              "[//]: # \"lastModified: 20217/13/10 23:59\"";

        var markDownBlogpostParser = new MarkdownPostParser(new Models.MardownFile(comments));
        await markDownBlogpostParser.ParseCommentsAsPropertiesAsync();

        Assert.True(markDownBlogpostParser.MarkdownPost.PubDate == DateTime.MinValue);
        Assert.True(markDownBlogpostParser.MarkdownPost.LastModified == DateTime.MinValue);
    }

    [Fact]
    public async Task Parse_Comments_To_A_Date()
    {
        var date = "13/10/2017 18:59";
        var comments = $"[//]: # \"pubDate: {date}\"\n";
        var markDownBlogpostParser = new MarkdownPostParser(new Models.MardownFile(comments));
        await markDownBlogpostParser.ParseCommentsAsPropertiesAsync();

        Assert.Equal(new DateTime(2017, 10, 13, 18, 59, 00), markDownBlogpostParser.MarkdownPost.PubDate);
    }

    [Fact]
    public async Task Parse_Comments_To_A_Date_1()
    {
        var date = "1/12/2023 18:59";
        var comments = $"[//]: # \"pubDate: {date}\"\n";
        var markDownBlogpostParser = new MarkdownPostParser(new Models.MardownFile(comments));
        await markDownBlogpostParser.ParseCommentsAsPropertiesAsync();

        Assert.Equal(new DateTime(2023, 12, 1, 18, 59, 00), markDownBlogpostParser.MarkdownPost.PubDate);
    }

    [Fact]
    public async Task Parse_Comments_To_Categories()
    {
        var comments = $"[//]: # \"categories: web,code,johnny,cars\"\n";
        var markDownBlogpostParser = new MarkdownPostParser(new Models.MardownFile(comments));
        await markDownBlogpostParser.ParseCommentsAsPropertiesAsync();

        Assert.True(markDownBlogpostParser.MarkdownPost.Categories.Count() == 4);
    }

    [Fact]
    public async Task Parse_Comments_To_Categories_Is_Empty()
    {
        var comments = $"[//]: # \"categories\"";
        var markDownBlogpostParser = new MarkdownPostParser(new Models.MardownFile(comments));
        await markDownBlogpostParser.ParseCommentsAsPropertiesAsync();

        Assert.True(markDownBlogpostParser.MarkdownPost.Categories.Count() == 0);
    }
}
