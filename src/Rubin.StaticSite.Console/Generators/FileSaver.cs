using Rubin.Static;

namespace Rubin.Markdown.Console.Generators;

public class FileSaver : ISaveOutput
{
    public async Task<string> SaveOutputAsync(string input, Slug identifier)
    {
        var path = string.Concat(
            AppDomain.CurrentDomain.BaseDirectory,
            "Views",
            Path.DirectorySeparatorChar,
            "Output",
            Path.DirectorySeparatorChar,
            identifier.Instance,
            ".html");

        await File.WriteAllTextAsync(path, input);

        return path;
    }
}
