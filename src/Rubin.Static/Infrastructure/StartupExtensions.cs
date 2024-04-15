using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;
using System.Reflection;
using Rubin.Static.Services;

namespace Rubin.Static.Infrastructure
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddStatic(this IServiceCollection services)
        {
            services.AddTransient<ICategoryPostOrganizer, CategoryPostsService>();
            services.AddTransient<ITransformMarkdownToHTML, TransformMarkdownToHTMLService>();
            services.AddTransient<IGenerateStatic, GenerateStaticService>();

            return services;
        }

        public static void AddRazorTemplating(this IServiceCollection services)
        {
            var fileProvider = new PhysicalFileProvider(AppContext.BaseDirectory);

            services.TryAddSingleton<DiagnosticSource>(new DiagnosticListener(Assembly.GetEntryAssembly()?.GetName().Name!));
            services.TryAddSingleton(new DiagnosticListener(Assembly.GetEntryAssembly()?.GetName().Name!));

            services.TryAddSingleton<IWebHostEnvironment>(new HostingEnvironment
            {
                ApplicationName = Assembly.GetEntryAssembly()?.GetName().Name!,
                ContentRootFileProvider = fileProvider
            });

            services.AddMvc().AddRazorRuntimeCompilation();

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationFormats.Add("/Views/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
            });
        }
    }
}
