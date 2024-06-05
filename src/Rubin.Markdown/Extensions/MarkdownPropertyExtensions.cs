using Rubin.Markdown.Models;

namespace Rubin.Markdown.Extensions
{
    public static class MarkdownPropertyExtensions
    {
        public static DateTime ParseToDate(this MarkdownProperty markdownProperty)
        {
            var formats = new string[] { 
                Constants.Config.DateTimeFormat1, 
                Constants.Config.DateTimeFormat2, 
                Constants.Config.DateTimeFormat3, 
                Constants.Config.DateTimeFormat4,
                Constants.Config.DateTimeFormat5,
                Constants.Config.DateTimeFormat6,
                Constants.Config.DateTimeFormat7,
                Constants.Config.DateTimeFormat8};

            if (DateTime.TryParseExact(
                markdownProperty.Value,
                formats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var date))
            {
                return date;
            }

            return DateTime.MinValue;
        }

        public static bool ParseToBool(this MarkdownProperty markdownProperty)
        {
            return bool.TryParse(markdownProperty.Value, out bool result) ? result : false;
        }
    }
}
