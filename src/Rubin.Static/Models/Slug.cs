using Rubin.Static.Extensions;

namespace Rubin.Static;

public class Slug
{
    public Slug(string fromText)
    {
        this.Instance = fromText;
        CreateSlug(fromText);
    }

    public string Instance { get; private set; }

    private void CreateSlug(string fromText)
    {
        this.Instance = this.GenerateSlug();
    }
}