using Rubin.Static;

namespace Rubin.Markdown.Console.Generators;

public interface ISaveOutput
{
    Task<string> SaveOutputAsync(string input, Slug identifier);
}
