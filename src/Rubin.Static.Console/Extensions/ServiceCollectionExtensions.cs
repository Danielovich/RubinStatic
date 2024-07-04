namespace Rubin.Markdown.Console.Extensions;

public static class ServiceCollectionExtensions
{
    public static void Setup(this IServiceCollection services)
    {
        // configure logging
        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });

        // build config
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        services.AddScoped<IConfiguration>(_ => configuration);
        services.AddTransient<IMapToPosts, PostService>();
        services.AddTransient<IGeneratePages, PageGenerator>();
        services.AddTransient<ISaveRenderedPage, SaveAsFile>();

        services.AddMarkdown();
        services.AddStatic();

        // Building provider due to the nature of how the default Renderer works
        // Not favourable, I know!
        var renderer = new Renderer(services.BuildServiceProvider());
        services.AddSingleton<IRenderer>(_ => renderer);

        services.AddTransient<Command, GenerateCommand>();
    }
}
