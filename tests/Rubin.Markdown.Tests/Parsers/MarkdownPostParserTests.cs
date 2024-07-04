namespace Rubin.Markdown.Tests.Parsers;

public class MarkdownPostParserTests
{
    [Fact]
    public async Task Parse_Comment_As_Post_Title_Property()
    {
        var comments =
              "[//]: # \"title: hugga bugga ulla johnson\"\n" +
              "[//]: # \"johnny: hugga bugga ulla johnson\"";

        var markdownPostParser = new MarkdownPostParser(new Models.MardownFile(comments));
        await markdownPostParser.ParseCommentsAsPropertiesAsync();

        Assert.Equal("hugga bugga ulla johnson", markdownPostParser.MarkdownPost.Title);
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

        var markdownPostParser = new MarkdownPostParser(new Models.MardownFile(comments));
        await markdownPostParser.ParseCommentsAsPropertiesAsync();

        Assert.True(markdownPostParser.MarkdownPost.Title != string.Empty);
        Assert.True(markdownPostParser.MarkdownPost.Slug != string.Empty);
        Assert.True(markdownPostParser.MarkdownPost.PubDate > DateTime.MinValue);
        Assert.True(markdownPostParser.MarkdownPost.LastModified > DateTime.MinValue);
        Assert.True(markdownPostParser.MarkdownPost.Excerpt != string.Empty);
        Assert.True(markdownPostParser.MarkdownPost.Categories.Count() == 4);
        Assert.True(markdownPostParser.MarkdownPost.IsPublished);
    }

    [Fact]
    public async Task Parsing_Checks_Comment_Formatting()
    {
        // there is a space in front of the last comment, which is not allowed
        var comments =
              "[//]: #   \"title: hugga bugga ulla johnson\" \n" +
              "[//]:#\"slug: hulla bulla\" \n " + 
              "[//]:# \"slug: hulla bulla 2\"";

        var markdownPostParser = new MarkdownPostParser(new Models.MardownFile(comments));
        await markdownPostParser.ParseCommentsAsPropertiesAsync();

        Assert.NotEmpty(markdownPostParser.MarkdownPost.Title);
        Assert.True(markdownPostParser.MarkdownPost.Slug.Equals("hulla bulla"));
    }

    [Fact]
    public async Task Parse_Comments_Fails_Due_To_Invalid_Comment_Format()
    {
        var comments =
              "[//]:   #\"title: hugga bugga ulla johnson\" \n" +
              "[//]:  # \"slug: hulla bulla\" \n";

        var markdownPostParser = new MarkdownPostParser(new Models.MardownFile(comments));
        await markdownPostParser.ParseCommentsAsPropertiesAsync();

        Assert.Empty(markdownPostParser.MarkdownPost.Title);
        Assert.Empty(markdownPostParser.MarkdownPost.Slug);
    }

    [Fact]
    public async Task Parse_Comments_With_Invalid_Dates_Returns_Default_Value()
    {
        var comments =
              "[//]: # \"pubDate: 20171/13/10 18:59\"\n" +
              "[//]: # \"lastModified: 20217/13/10 23:59\"";

        var markdownPostParser = new MarkdownPostParser(new Models.MardownFile(comments));
        await markdownPostParser.ParseCommentsAsPropertiesAsync();

        Assert.True(markdownPostParser.MarkdownPost.PubDate == DateTime.MinValue);
        Assert.True(markdownPostParser.MarkdownPost.LastModified == DateTime.MinValue);
    }

    [Fact]
    public async Task Parse_Comments_To_A_Date()
    {
        var date = "13/10/2017 18:59";
        var comments = $"[//]: # \"pubDate: {date}\"\n";
        var markdownPostParser = new MarkdownPostParser(new Models.MardownFile(comments));
        await markdownPostParser.ParseCommentsAsPropertiesAsync();

        Assert.Equal(new DateTime(2017, 10, 13, 18, 59, 00), markdownPostParser.MarkdownPost.PubDate);
    }

    [Fact]
    public async Task Parse_Comments_To_A_Date_1()
    {
        var date = "01/12/2023 18:59";
        var comments = $"[//]: # \"pubDate: {date}\"\n";
        var markdownPostParser = new MarkdownPostParser(new Models.MardownFile(comments));
        await markdownPostParser.ParseCommentsAsPropertiesAsync();

        Assert.Equal(new DateTime(2023, 12, 1, 18, 59, 00), markdownPostParser.MarkdownPost.PubDate);
    }

    [Fact]
    public async Task Parse_Comments_To_A_Date_2()
    {
        var date = "05/29/2023 18:59";
        var comments = $"[//]: # \"pubDate: {date}\"\n";
        var markdownPostParser = new MarkdownPostParser(new Models.MardownFile(comments));
        await markdownPostParser.ParseCommentsAsPropertiesAsync();

        Assert.Equal(new DateTime(2023, 5, 29, 18, 59, 00), markdownPostParser.MarkdownPost.PubDate);
    }

    [Fact]
    public async Task Parse_Comments_To_Categories()
    {
        var comments = $"[//]: # \"categories: web,code,johnny,cars\"\n";
        var markdownPostParser = new MarkdownPostParser(new Models.MardownFile(comments));
        await markdownPostParser.ParseCommentsAsPropertiesAsync();

        Assert.True(markdownPostParser.MarkdownPost.Categories.Count() == 4);
    }

    [Fact]
    public async Task Parse_Comments_To_Categories_Is_Empty()
    {
        var comments = $"[//]: # \"categories\"";
        var markdownPostParser = new MarkdownPostParser(new Models.MardownFile(comments));
        await markdownPostParser.ParseCommentsAsPropertiesAsync();

        Assert.True(markdownPostParser.MarkdownPost.Categories.Count() == 0);
    }
}
