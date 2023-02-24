using System.Collections.Generic;

namespace RepoBuilder
{
    /// <summary> 
    /// Extention type of the file.
    /// Edit it as you see fit.
    /// Just don't forget to update the IDictionary as well.
    /// </summary>
    public enum Extention { CSharp, HTML, CSS, JavaScript, Text, Bat, Python, Markdown, None }

    
    internal static class EnumToString
    {

        private static string KeyByValue(this IDictionary<string, Extention> dict, Extention val)
        {
            string key = default;
            foreach (KeyValuePair<string, Extention> pair in dict)
            {
                if (EqualityComparer<Extention>.Default.Equals(pair.Value, val))
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
        public static string ToExtentionString(this Extention extentionValue) => extentionDic.KeyByValue(extentionValue);


        /// <summary>
        /// Converts the string value into its respective extention type enum.
        /// (Eg. Extention.JavaScript returns ".js").
        /// </summary>
        /// <param name="extentionValue">The enum value to be converted.</param>
        /// <returns></returns>
        public static Extention ToExtentionEnum(this string extention) => extentionDic[extention];
    }
}
