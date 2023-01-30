using System;

namespace FileBuilder
{
    /// <summary> Preset folders. </summary>
    public enum Folder
    {
        ApplicationData,
        CommonApplicationData,
        CommonProgramFiles,
        Cookies,
        Desktop,
        DesktopDirectory,
        Favorites,
        History,
        InternetCache,
        MyComputer,
        MyDocuments,
        MyMusic,
        MyPictures,
        Personal,
        ProgramFiles,
        Programs,
        Recent,
        SendTo,
        StartMenu,
        Startup,
        System,
        Templates,
    }
    public static class FolderToString
    {
        /// <summary> Converts the Folder enum value into valid  string. </summary>
        /// <param name="folder"></param>
        /// <returns>Valid path string.</returns>
        public static string ToLocationString(this Folder folder) {
            // getting environment namespace enum folder value through our custom enum by convertion
            Environment.SpecialFolder convertedEnumValue = (Environment.SpecialFolder)Enum.Parse(typeof(Environment.SpecialFolder), folder.ToString());
            // getting path string through this converted enum value
            return Environment.GetFolderPath(convertedEnumValue);
        }
    }
}
