namespace RepoBuilder
{
    /// <summary> 
    /// Extention type of the file.
    /// Edit it as you see fit.
    /// Just don't forget to update the ToExtentionString and ToExtentionEnum methods as well.
    /// </summary>
    public enum Extention { CSharp, HTML, CSS, JavaScript, Text, Bat, Python, None }

    internal static class EnumToString
    {

        /// <summary>
        /// Converts the extention type enum value into its respective string.
        /// Eg. (Extention.JavaScript) => ".js"
        /// </summary>
        /// <param name="extentionValue">The enum value to be converted.</param>
        /// <returns></returns>
        internal static string ToExtentionString(this Extention extentionValue)
        {
            switch (extentionValue)
            {
                case Extention.CSharp: return "cs";
                case Extention.HTML: return "html";
                case Extention.CSS: return "css";
                case Extention.JavaScript: return "js";
                case Extention.Text: return "txt";
                case Extention.Bat: return "bat";
                case Extention.Python: return "py";
                default: return "";
            }
        }


        /// <summary>
        /// Converts the string value into its respective extention type enum.
        /// (Eg. Extention.JavaScript returns ".js").
        /// </summary>
        /// <param name="extentionValue">The enum value to be converted.</param>
        /// <returns></returns>
        internal static Extention ToExtentionEnum(this string extention)
        {
            switch (extention)
            {
                case "cs": return Extention.CSharp;
                case "html": return Extention.HTML;
                case "css": return Extention.CSS;
                case "js": return Extention.JavaScript;
                case "txt": return Extention.Text;
                case "bat": return Extention.Bat;
                case "py": return Extention.Python;
                default: return Extention.None;
            }
        }
    }
}
