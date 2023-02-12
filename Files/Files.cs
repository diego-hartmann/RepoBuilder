using System.IO;
using System.Windows.Markup;

namespace FileBuilder
{
    public abstract class Files : Blueprint
    {

        internal void ConstructorForFile(string name, Extention extention)
        {
            Name = name;
            Extention = extention;
            unbuildName = name;
            unbuildFileExtentionText = extention.ToExtentionString();
        }

        #region ==== PRIVATE FIELDS ==============
        private Extention extention;
        private string fileExtentionText;
        #endregion -------------------------------



        #region =========== PUBLIC PROPERTIES ====================================================

        /// <summary> Number of copies made with GetCopy method (readonly). </summary>
        public int NumberOfCopies { get; protected set; }

        /// <summary> Extention type of the file. </summary>
        public Extention Extention
        {
            get => extention;
            set
            {
                extention = value;
                fileExtentionText = extention.ToExtentionString();
            }
        }

        /// <summary> Complete path of the file, including location, name and extention (readonly). </summary>
        public override string Path => $"{Location}/{Name}.{fileExtentionText}";

        /// <summary> The content inside the file (readonly). </summary>
        public string Content { get; private set; } = string.Empty;

        #endregion ===============================================================================================



        #region =========== PRIVATE METHODS ==============================================================

        internal override void CheckForExistence()
        {
            // DOES NOT PREVIUOUS EXIST
            if (!File.Exists(Path)) return;

            // PREVIUOUS EXISTS
            // getting the the real file content
            StreamReader fileReader = new StreamReader(Path);
            string fileContent = fileReader.ReadToEnd();
            fileReader.Close();

            // filling the object content with the real file content
            AddContent(fileContent);

            //saying to the algorithm that the file is already built
            IsBuilt = true;
        }
        #endregion =======================================================================================




        #region =========== PUBLIC METHODS ===============================================================

        /// <summary> Adds text to the Content property. </summary>
        /// <param name="content">The content string to be added.</param>
        public void AddContent(string content) => Content += content;

        /// <summary> Adds text line to the Content property. </summary>
        /// <param name="content">The content string line to be added.</param>
        public void AddContentLine(string content)
        {
            if (Content.Length > 0)
            {
                Content += $"\n{content}";
                return;
            }
            Content += content;
        }

        /// <summary> Makes Content property empty. </summary>
        public void ClearContent() => Content = string.Empty;
        #endregion ------------------------------------------------------------------------------------------




        #region =========== INTERNAL METHODS ===============================================================

        /// <summary> Creates the real file or updates the existing one. </summary>
        internal override void OnBuild()
        {
            this.unbuildFileExtentionText = this.fileExtentionText;
            StreamWriter writer = new StreamWriter(Path, false);
            writer.Write(Content);
            writer.Close();
        }

        /// <summary> Deletes the real file. </summary>
        /// <param name="fileBlueprint">The blueprint of the real file to be deleted.</param>
        internal override void OnUnbuild() => File.Delete(UnbuildPath);
        #endregion =======================================================================================
    }
}
