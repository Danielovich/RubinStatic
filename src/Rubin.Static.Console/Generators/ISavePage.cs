namespace Rubin.Markdown.Console.Generators;

public interface ISavePage
{
    Task<string> Save(RenderedPage rendering);
    Task<IEnumerable<string>> Save(IEnumerable<RenderedPage> renderings);
}
