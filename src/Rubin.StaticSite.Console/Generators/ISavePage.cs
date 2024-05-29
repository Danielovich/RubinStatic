using Rubin.Static;
using Rubin.Static.Models;

namespace Rubin.Markdown.Console.Generators;

public interface ISavePage
{
    Task<string> Save(string input, Slug identifier);
}
