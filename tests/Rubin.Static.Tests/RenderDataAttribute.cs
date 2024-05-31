namespace Rubin.Static.Tests;

public class RenderDataAttribute : AutoDataAttribute
{
    public RenderDataAttribute() : base(() => new Fixture().Customize(new RenderCustomization())) { }
}
