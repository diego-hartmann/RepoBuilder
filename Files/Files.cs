using System.IO;

namespace FileBuilder
{
    public abstract class Files : Blueprint
    {


        #region ===========- CONSTRUCTOR HELPER -========================================================
        internal void ConstructorForFile(string name, Extention extention)
        {
            Name = name;
            Extention = extention;
            unbuildName = name;
            unbuildFileExtentionText = extention.ToExtentionString();
        }
        #endregion ______________________________________________________________________________________








        #region ===========- PRIVATE FIELDS -============================================================
        private Extention extention;
        private string fileExtentionText;
        #endregion ______________________________________________________________________________________









        #region ===========- PUBLIC PROPERTIES -=========================================================
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

        #endregion _______________________________________________________________________________________









        #region ===========- PUBLIC METHODS -=============================================================
        /// <summary> Adds text to the Content property. </summary>
        /// <param name="content">The content string to be added.</param>
        public void AddContent(string content) => Content += content;

        /// <summary> Adds text line to the Content property. </summary>
        /// <param name="content">The content string line to be added.</param>
        public void AddContentLine(string content)
        {
            // if there is any content inside Content string...
            if (Content.Length > 0)
            {
                // it will break the last written line.
                Content += $"\n{content}";
                return;
            }

            // otherwise, it will just sums on the Content without leaving the first line blank by braking it.
            Content += content;
        }

        /// <summary> Makes Content property empty. </summary>
        public void ClearContent() => Content = string.Empty;
        #endregion _______________________________________________________________________________________









        #region ===========- INTERNAL METHODS -===========================================================
        internal override void CheckForExistence()
        {
            // DOES NOT PREVIUOUSLY EXIST
            if (!File.Exists(Path)) return;

            // PREVIUOUSLY EXISTS
            // getting the the real file content
            using (StreamReader fileReader = new StreamReader(Path))
            {
                string fileContent = fileReader.ReadToEnd();
                fileReader.Close();
                // filling the object content with the real file content
                AddContent(fileContent);
            }

            // saying to the algorithm that the file is already built.
            IsBuilt = true;
        }

        internal override void OnBuild()
        {
            // updating the unbuild extention text field.
            this.unbuildFileExtentionText = this.fileExtentionText;
            
            // instantiating readonly IDisposable object to operate on it.
            using(StreamWriter writer = new StreamWriter(Path, false)){
                
                // writing the real file content based on the blueprint's Content string.
                writer.Write(Content);

                // closing the real file.
                writer.Close();
            }
        }

        internal override void OnUnbuild() => File.Delete(UnbuildPath);
        #endregion _______________________________________________________________________________________

    }
}
