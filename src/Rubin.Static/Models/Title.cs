namespace Rubin.Static.Models;

public class Title
{
    public Title()
    {

    }
    public Title(string title)
    {
        PostTitle = title;
    }

    public string PostTitle { get; set; } = string.Empty;
}