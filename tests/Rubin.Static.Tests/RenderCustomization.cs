using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using Rubin.Static.Infrastructure;
using Rubin.Static.Services;

namespace Rubin.Static.Tests
{
    public class RenderCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            ServiceCollection sc = new ServiceCollection();
            sc.AddRazorTemplating();

            var renderer = new Renderer(sc.BuildServiceProvider());

            fixture.Register(() => new GenerateStaticService(renderer));
        }
    }
}
