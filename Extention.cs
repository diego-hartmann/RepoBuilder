namespace FileBuilder
{
    /// <summary> Extention type of the file. </summary>
    public enum Extention { CSharp, HTML, CSS, JavaScript, Text, Bat, Python }

    internal static class EnumToString
    {
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
    }
}
