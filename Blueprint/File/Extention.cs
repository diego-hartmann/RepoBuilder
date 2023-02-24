using System.Collections.Generic;

namespace RepoBuilder
{
    /// <summary> 
    /// Extention type of the file.
    /// Edit it as you see fit.
    /// Just don't forget to update the ToExtentionString and ToExtentionEnum methods as well.
    /// </summary>
    public enum Extention { CSharp, HTML, CSS, JavaScript, Text, Bat, Python, Markdown, None }

    
    internal static class EnumToString
    {


        private static T KeyByValue<T, W>(this IDictionary<T, W> dict, W val)
        {
            T key = default;
            foreach (KeyValuePair<T, W> pair in dict)
            {
                if (EqualityComparer<W>.Default.Equals(pair.Value, val))
                {
                    key = pair.Key;
                    break;
                }
            }
            return key;
        }

        private static IDictionary<string, Extention> extentionDic = new Dictionary<string, Extention>()
        {
            { "cs", Extention.CSharp },
            { "html", Extention.HTML },
            { "css", Extention.CSS },
            { "js", Extention.JavaScript },
            { "txt", Extention.Text },
            { "bat", Extention.Bat },
            { "py", Extention.Python },
            { "md", Extention.Markdown },
            { "", Extention.None },
        };


        /// <summary>
        /// Converts the extention type enum value into its respective string.
        /// Eg. (Extention.JavaScript) => ".js"
        /// </summary>
        /// <param name="extentionValue">The enum value to be converted.</param>
        /// <returns></returns>
        public static string ToExtentionString(this Extention extentionValue)
        {
            return extentionDic.KeyByValue(extentionValue);

            //switch (extentionValue)
            //{
            //    case Extention.CSharp: return "cs";
            //    case Extention.HTML: return "html";
            //    case Extention.CSS: return "css";
            //    case Extention.JavaScript: return "js";
            //    case Extention.Text: return "txt";
            //    case Extention.Bat: return "bat";
            //    case Extention.Python: return "py";
            //    case Extention.Markdown: return "md";
            //    default: return "";
            //}
        }


        /// <summary>
        /// Converts the string value into its respective extention type enum.
        /// (Eg. Extention.JavaScript returns ".js").
        /// </summary>
        /// <param name="extentionValue">The enum value to be converted.</param>
        /// <returns></returns>
        public static Extention ToExtentionEnum(this string extention)
        {
            return extentionDic[extention];

            //switch (extention)
            //{
            //    case "cs": return Extention.CSharp;
            //    case "html": return Extention.HTML;
            //    case "css": return Extention.CSS;
            //    case "js": return Extention.JavaScript;
            //    case "txt": return Extention.Text;
            //    case "bat": return Extention.Bat;
            //    case "py": return Extention.Python;
            //    case "md": return Extention.Markdown;
            //    default: return Extention.None;
            //}
        }
    }
}
