using System.IO;

namespace FileBuilder
{
    public abstract class Files
    {

        internal void Constructor(string name, Extention extention)
        {
            Name = name;
            Extention = extention;
            unbuildName = name;
            unbuildFileExtentionText = extention.ToExtentionString();
        }


        #region =========== PRIVATE FIELDS ===============================================================
        private Extention extention;
        private string fileExtentionText;

        // these three data hold the last saved information to correctly unbuild the old version of the file through its old Path (UnbuildPath bellow).
        // they will be updated into the Build method as the last path version saved.
        private string unbuildLocation;
        private string unbuildName;
        private string unbuildFileExtentionText;
        // called on Unbuild method;
        private string UnbuildPath => $"{unbuildLocation}/{unbuildName}.{unbuildFileExtentionText}";
        #endregion =======================================================================================





        #region =========== PUBLIC PROPERTIES ====================================================

        /// <summary> Number of copies made with GetCopy method (readonly). </summary>
        public int NumberOfCopies { get; protected set; }

        /// <summary> Specifies if this file is currently mounted with the Build method (readonly). </summary>
        public bool IsBuilt { get; internal set; }


        /// <summary> Specifies if this file has folder parent. </summary>
        public bool HasFolderParent => FolderParent != null;

        /// <summary> The folder parent of the file. </summary>
        public Folders FolderParent { get; internal set; }

        /// <summary> Location of the file. </summary>
        public string Location => FolderParent?.Path ?? null;


        /// <summary> Name of the file. </summary>
        public string Name { get; set; }

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
        public string Path => $"{Location}/{Name}.{fileExtentionText}";

        /// <summary> The content inside the file (readonly). </summary>
        public string Content { get; private set; } = string.Empty;

        #endregion ===============================================================================================




        #region =========== PRIVATE METHODS ==============================================================

        internal bool CheckForExistingFile()
        {
            if (!File.Exists(Path)) return false;

            // gettingthe the real file content
            StreamReader fileReader = new StreamReader(Path);
            string fileContent = fileReader.ReadToEnd();
            fileReader.Close();

            // filling the object content with the real file content
            AddContent(fileContent);

            //saying to the algorithm that the file is already built
            IsBuilt = true;
            return true;
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
        public void ClearContent() => Content = "";
        #endregion ------------------------------------------------------------------------------------------




        #region =========== INTERNAL METHODS ===============================================================

        /// <summary> Creates the real file or updates the existing one. </summary>
        internal void Build()
        {
            Unbuild();

            // updating the UnbuildPath property through the three fields that compose it.  
            this.unbuildLocation = this.Location;
            this.unbuildName = this.Name;
            this.unbuildFileExtentionText = this.fileExtentionText;

            StreamWriter writer = new StreamWriter(Path, false);
            writer.Write(Content);
            writer.Close();
            IsBuilt = true;
        }

        /// <summary> Deletes the real file. </summary>
        /// <param name="fileBlueprint">The blueprint of the real file to be deleted.</param>
        internal void Unbuild()
        {
            if (!IsBuilt) return;
            // using the update UnbuildPath to delete the last version of the file
            File.Delete(UnbuildPath);
            IsBuilt = false;
        }
        #endregion =======================================================================================
    }
}
