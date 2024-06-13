namespace Rubin.Markdown.Console.Generators;

public class SaveAsFile : ISavePage
{
    public async Task<string> Save(RenderedPage rendering)
    {
        // save it at bin/{debug|release}/{.net version}/Views/Output/*.html
        var path = string.Concat(
            AppDomain.CurrentDomain.BaseDirectory,
            "Views",
            Path.DirectorySeparatorChar,
            "Output",
            Path.DirectorySeparatorChar,
            rendering.Slug.Instance,
            ".html");

        await File.WriteAllTextAsync(path, rendering.Content);

        return path;
    }

    public async Task<IEnumerable<string>> Save(IEnumerable<RenderedPage> renderings)
    {
        var result = new List<string>();

        foreach (var item in renderings)
        {
            result.Add(
                await Save(item)
            );
        }

        return result;
    }
}
