﻿using System;
using System.IO;

namespace FileBuilder
{
    public class FileBlueprint
    {
        /// <summary> Creates a virtual file that can be edited before being really created into your local machine. </summary>
        /// <param name="location">The path the real file will be created in.</param>
        /// <param name="name">The name the real file will have.</param>
        /// <param name="extention">The extention of the file.</param>
        public FileBlueprint(string location, string name, Extention extention)
        {
            this.Location = location;
            this.Name = name;
            this.Extention = extention;

            // checks if file already exists
            IsFileBuilded = File.Exists(Path);
            unbuildLocation = location;
        }

        /// <summary> Creates a virtual file that can be edited before being really created into your local machine. </summary>
        /// <param name="specialFolder">The SpecialFolder enum value containing the path that the real file will be created in.</param>
        /// <param name="name">The name the real file will have.</param>
        /// <param name="extention">The extention of the file.</param>
        public FileBlueprint(SpecialFolder specialFolder, string name, Extention extention)
        {
            this.Name = name;
            this.Extention = extention;

            // getting environment namespace enum folder value through our custom enum by convertion
            Environment.SpecialFolder convertedEnumValue = (Environment.SpecialFolder)Enum.Parse(typeof(Environment.SpecialFolder), specialFolder.ToString());
            // getting path string through this enum value
            this.Location = Environment.GetFolderPath(convertedEnumValue);

            // checks if file already exists
            IsFileBuilded = File.Exists(Path);
            unbuildLocation = location;
        }

      



        #region =========== PRIVATE FIELDS ===============================================================
        private string location;
        private Extention extention;
        private string fileExtentionText;
        private string unbuildLocation;
        private string UnbuildPath => $"{unbuildLocation}/{Name}.{fileExtentionText}";
        #endregion =======================================================================================




        #region =========== PUBLIC PROPERTIES FIELDS =====================================================

        /// <summary> Number of copies made with GetCopy method (readonly). </summary>
        public int NumberOfCopies { get; private set; }

        /// <summary> Specifies if this file is currently mounted with the Build method (readonly). </summary>
        public bool IsFileBuilded { get; private set; }

        /// <summary> Location of the file. </summary>
        public string Location
        {
            get => location;
            set => location = value.Replace("\\", "/");
        }

        /// <summary> Name of the file. </summary>
        public string Name { get; set; }

        /// <summary> Extention type of the file. </summary>
        public Extention Extention
        {
            get => extention;
            set
            {
                extention = value;
                SetFileExtentionTextBasedOnEnum(value);
            }
        }

        /// <summary> Complete path of the file, including location, name and extention (readonly). </summary>
        public string Path => $"{Location}/{Name}.{fileExtentionText}";

        /// <summary> The content inside the file (readonly). </summary>
        public string Content { get; private set; } = "";

        #endregion =======================================================================================


        #region =========== PRIVATE METHODS ==============================================================
        private void SetFileExtentionTextBasedOnEnum(Extention value) {
            switch (value) {
                case Extention.CSharp: this.fileExtentionText = "cs"; break;
                case Extention.HTML: this.fileExtentionText = "html"; break;
                case Extention.CSS: this.fileExtentionText = "css"; break;
                case Extention.JavaScript: this.fileExtentionText = "js"; break;
                case Extention.Text: this.fileExtentionText = "txt"; break;
                case Extention.Bat: this.fileExtentionText = "bat"; break;
                case Extention.Python: this.fileExtentionText = "py"; break;
                default: this.fileExtentionText = ""; break;
            }
        }
        #endregion =======================================================================================


        #region =========== PUBLIC METHODS ===============================================================

        /// <summary> Adds text to the Content property. </summary>
        /// <param name="content">The content string to be added.</param>
        public void AddContent(string content) => Content += content;

        /// <summary> Adds text line to the Content property. </summary>
        /// <param name="content">The content string line to be added.</param>
        public void AddContentLine(string content) {
            if (Content.Length > 0) {
                Content += $"\n{content}";
                return;
            }
            Content += content;
        }

        /// <summary> Makes Content property empty. </summary>
        /// <param name="content">The content string line to be added.</param>
        public void ClearContent() => Content = "";


        /// <summary> Creates the real file or updates the existing one. </summary>
        public void Build() {
            Unbuild();
            unbuildLocation = location;
            StreamWriter writer = new StreamWriter(Path, false);
            writer.Write(Content);
            writer.Close();
            IsFileBuilded = true;
        }

        /// <summary> Deletes the real file. </summary>
        /// <param name="fileBlueprint">The blueprint of the real file to be deleted.</param>
        public void Unbuild() {
            if (!IsFileBuilded) return;
            File.Delete(UnbuildPath);
            IsFileBuilded = false;
        }

        /// <summary> Creates another blueprint based on this. </summary>
        /// <returns>Returns a new object with the same properties.</returns>
        public FileBlueprint GetCopy() {
            NumberOfCopies++;
            string newName = $"{Name}_{NumberOfCopies}";
            FileBlueprint duplication = new FileBlueprint(Location, newName, Extention);
            duplication.AddContent(Content);
            return duplication;
        }

        #endregion =======================================================================================

    }
}
