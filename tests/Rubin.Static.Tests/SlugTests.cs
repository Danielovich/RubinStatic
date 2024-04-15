namespace Rubin.Static.Tests;

public class SlugTests
{
    [Theory]
    [InlineData("hulla bulla ulla", "hulla-bulla-ulla")]
    [InlineData("hulla-bulla-ulla", "hulla-bulla-ulla")]
    [InlineData("ÅøæÆååøÆåæø!!djijasd", "djijasd")]
    [InlineData("Johnny Er /2633--1%", "johnny-er-2633--1")]
    public void Create_Valid_Slugs(string actual, string expected)
    {
        var slug = new Slug(actual);

        var instance = slug.Instance;

        Assert.Equal(expected, instance.ToString());
    }
}
