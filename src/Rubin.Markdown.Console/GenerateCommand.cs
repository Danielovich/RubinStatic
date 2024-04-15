using System.CommandLine;
using System.Globalization;
using Rubin.Markdown.Console.Generators;
using Rubin.Markdown.Console.Mappers;
using Rubin.Static.Infrastructure;
using Rubin.Static.Models;

namespace Rubin.Markdown.Console;

/// <summary>
/// Using System.CommandLine for this command line tool
/// This command is the initializer for the static content generator
/// <see cref="https://learn.microsoft.com/en-us/dotnet/standard/commandline/"/>
/// </summary>
public class GenerateCommand : Command
{
    private readonly IGeneratePages pageGenerator;
    private readonly IMapToPosts postMapper;

    public GenerateCommand(
        IGeneratePages pageGenerator,
        IMapToPosts postMapper) : base("generate")
    {
        // explicit set this here so we can control the output of dates and times
        Thread.CurrentThread.CurrentCulture = new CultureInfo("da-DK");

        this.pageGenerator = pageGenerator;
        this.postMapper = postMapper;

        this.SetHandler(Execute);
    }

    private async Task Execute()
    {
        var posts = await postMapper.MapToPostsAsync();

        await SetupLayoutHelper( posts );
        await pageGenerator.GenerateCategoryPage( posts );
        await pageGenerator.GenerateNoCategoryPage( posts, "Uncategorized" );
        await pageGenerator.GeneratePostPage( posts );
        await pageGenerator.GenerateIndexPage( posts );
        await pageGenerator.GenerateAllPage(posts);
    }

    /// <summary>
    /// Since we are utilizing Ravor layout views and perhaps wish to push a model to that layout, which cannot be done explicitly,
    /// we have to do some tricks to enable some kind of modelling. Here we are setting up a singletong Layout helper which has 
    /// other static methods to be used from the Layout view.
    /// </summary>
    /// <param name="posts"></param>
    /// <seealso cref="LayoutHelper"/>
    private async Task SetupLayoutHelper(IEnumerable<Post> posts)
    {
        await LayoutHelper.Instance.Initialize(posts);
    }
}
