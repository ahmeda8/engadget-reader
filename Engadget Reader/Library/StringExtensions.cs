using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Engadget_Reader
{
    class StringExtensions
    {
        public static string Cleanup(string str)
        {
            if (str == null)
                return string.Empty;
            str = str.Trim();
            Regex html = new Regex("<([^>]+)>");
            Regex multiple_spaces = new Regex("(" + Regex.Escape(" ") + "{2,}" + ")");
            Regex multiple_newline = new Regex(Regex.Escape("\n") + "{2,}");
            str = html.Replace(str, " ");
            str = multiple_newline.Replace(str, "");
            str = multiple_spaces.Replace(str, " ");
            
            return str;
        }

        public static string ExtractImageLocation(string str)
        {
            if (str == null)
                return string.Empty;
            Regex img = new Regex("<img[^>]+/>");
            Regex src = new Regex("src=\"(.*)\"");
            str = img.Match(str).Value;
            str = src.Match(str).Value;
            str = str.Substring(5, str.Length - 6);
            return str;
        }
    }
}
