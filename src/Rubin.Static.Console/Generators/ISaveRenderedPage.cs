namespace Rubin.Markdown.Console.Generators;

public interface ISaveRenderedPage
{
    Task<string> Save(RenderedPage rendering);
    Task<IEnumerable<string>> Save(IEnumerable<RenderedPage> renderings);
}
