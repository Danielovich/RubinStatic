namespace Rubin.Markdown.Tests;

using System;

public class MarkdownUriAutoData : AutoDataAttribute
{
    public MarkdownUriAutoData() : base(CreateMarkdownUriFixture)
    {
    }

    private static IFixture CreateMarkdownUriFixture()
    {
        var fixture = new Fixture();
        fixture.Customize<Uri>(c => c.FromFactory(() => new Uri("https://some.dk/domain/" + Guid.NewGuid() + ".md")));

        return fixture;
    }
}
