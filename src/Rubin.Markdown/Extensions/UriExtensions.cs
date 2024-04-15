namespace Rubin.Markdown.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class UriExtensions
    {
        public static IEnumerable<Uri> ByValidFileExtensions(this IEnumerable<Uri> uris, string[] validExtensions)
        {
            foreach (var uri in uris)
            {
                for (var i = 0; i < validExtensions.Length; i++)
                {
                    if (validExtensions[i] == uri.GetFileExtension())
                    {
                        yield return uri;
                        break;
                    }
                }
            }
        }

        public static string GetFileExtension(this Uri uri)
        {
            if (uri == null)
            {
                return string.Empty;
            }

            string path = uri.AbsoluteUri;
            string fileName = Path.GetFileName(path);

            if (fileName.Contains('.'))
            {
                return Path.GetExtension(fileName);
            }

            return string.Empty;
        }
    }
}
