using System.Text.RegularExpressions;

namespace Rubin.Static.Extensions;

//https://gist.github.com/fhferreira/ae35632b0325916a24a1
public static class SlugExtensions
{
    public static string GenerateSlug(this Slug slug)
    {
        string str = slug.Instance.RemoveAccent().ToLower();

        str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // invalid chars           
        str = Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space
        str = str.Substring(0, str.Length <= 1024 ? str.Length : 1024).Trim(); // cut and trim it
        str = Regex.Replace(str, @"\s", "-"); // hyphens   

        return str;
    }

    public static string RemoveAccent(this string txt)
    {
        byte[] bytes = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(txt);
        return System.Text.Encoding.ASCII.GetString(bytes);
    }

    public static string ToUri(this Slug slug)
    {
        return string.Concat(slug.Instance, ".html");
    }
}