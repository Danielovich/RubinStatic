using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rubin.Markdown.Console.Generators;
using Rubin.Markdown.Console.Mappers;
using Rubin.Markdown.Extensions;
using Rubin.Static.Infrastructure;
using Rubin.Static.Services;
using System.CommandLine;

namespace Rubin.Markdown.Console.Extensions
{
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
            services.AddTransient<ISaveOutput, FileSaver>();

            services.AddMarkdown();
            services.AddRazorTemplating();
            services.AddStatic();

            // Building provider due to the nature of how the default Renderer works
            // Not favourable, I know!
            var renderer = new Renderer(services.BuildServiceProvider());
            services.AddSingleton<IRenderer>(_ => renderer);

            services.AddTransient<Command, GenerateCommand>();
        }
    }
}
