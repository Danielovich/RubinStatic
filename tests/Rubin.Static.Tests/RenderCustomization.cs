namespace Rubin.Static.Tests;

public class RenderCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        ServiceCollection sc = new ServiceCollection();
        sc.AddStatic();

        var renderer = new Renderer(sc.BuildServiceProvider());

        fixture.Register(() => new PageRendering(renderer));
    }
}
