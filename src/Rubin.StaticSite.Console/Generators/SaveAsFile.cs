using Rubin.Static;
using Rubin.Static.Models;

namespace Rubin.Markdown.Console.Generators;

public class SaveAsFile : ISavePage
{
    public async Task<string> Save(string content, Slug identifier)
    {
        // save it at bin/{debug|release}/{.net version}/Views/Output/*.html
        var path = string.Concat(
            AppDomain.CurrentDomain.BaseDirectory,
            "Views",
            Path.DirectorySeparatorChar,
            "Output",
            Path.DirectorySeparatorChar,
            identifier.Instance,
            ".html");

        await File.WriteAllTextAsync(path, content);

        return path;
    }
}
