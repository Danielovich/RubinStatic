using AutoFixture;
using AutoFixture.Xunit2;

namespace Rubin.Static.Tests
{
    public class RenderDataAttribute : AutoDataAttribute
    {
        public RenderDataAttribute() : base(() => new Fixture().Customize(new RenderCustomization())) { }
    }
}
