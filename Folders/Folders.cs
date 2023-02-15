using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileBuilder
{

    public abstract class Folders : Blueprint
    {

        #region =========== PROTECTED FIELDS ====================
        protected string location = String.Empty;
        protected string unbuildLPath => $"{unbuildLocation}/{unbuildName}";
        protected bool isContainer = false;
        #endregion --------------------------------------------


        #region =========== PUBLIC PROPERTIES =================
        
        /// <summary> Location of the file. </summary>

        public override string Location
        {
            get => FolderParent?.Path ?? null;
            // for containers, it must have a setter (see ConstructorForContainer()).
            protected set
            {
                StringBuilder _value = new StringBuilder(value);
                _value.Replace("\\", "/");
                _value.Replace("//", "/");
                location = _value.ToString();
            }
        }

        public override string Path => $"{Location}/{Name}" ?? null;
        public List<Files> ChildFileList { get; protected set; } = new List<Files>();
        public List<Folders> ChildFolderList { get; protected set; } = new List<Folders>();
        #endregion --------------------------------------------



        #region =========== PUBLIC METHODS ====================
        public void Clear()
        {
            ChildFileList.Clear();
            ChildFolderList.Clear();
        }
        public void Add(Files fileBlueprint)
        {
            // same blueprint...
            if (ChildFileList.Contains(fileBlueprint)) return;
            // same file assignature...
            foreach (var item in ChildFileList)
            {
                if (item.Name == fileBlueprint.Name && item.Extention == fileBlueprint.Extention)
                {
                    return;
                }
            }
            // ok, you can add.
            fileBlueprint.CheckForExistence();
            fileBlueprint.FolderParent = this;
            ChildFileList.Add(fileBlueprint);
        }
        public void Remove(Files fileBlueprint)
        {
            if (ChildFileList.Contains(fileBlueprint))
            {
                fileBlueprint.FolderParent = null;
                ChildFileList.Remove(fileBlueprint);
                fileBlueprint.IsBuilt = false;
            }
        }
        public void Add(Folders folderBlueprint)
        {
            // same blueprint...
            if (ChildFolderList.Contains(folderBlueprint)) return;
            // same assignature...
            foreach (var item in ChildFolderList)
            {
                if (item.Name == folderBlueprint.Name) return;
            }
            // ok you can add.
            folderBlueprint.FolderParent = this;
            folderBlueprint.CheckForExistence();
            ChildFolderList.Add(folderBlueprint);
        }
        public void Remove(Folders folderBlueprint)
        {
            if (ChildFolderList.Contains(folderBlueprint))
            {
                folderBlueprint.FolderParent = null;
                ChildFolderList.Remove(folderBlueprint);
                FolderParent.IsBuilt = false;
            }
        }
       

        #endregion --------------------------------------------




        #region ========= INTERNAL METHODS =========================
        internal override void OnBuild()
        {
            Directory.CreateDirectory(Path);
            BuildAllContent();
        }

        internal override void OnUnbuild()
        {
            Directory.Delete(unbuildLPath, true);
            UnbuildAllContent();
        }
        #endregion -------------------------------------------------




        #region =========== PROTECTED METHODS ==================

        private void CheckForSelfExistence(string _path)
        {
            if (!Directory.Exists(_path)) return;

            //saying to the algorithm that the container is already built
            this.IsBuilt = true;
        }
        private void CheckForContentExistence_Files(string __path)
        {
            var files = Directory.GetFiles(__path);
            foreach (string file in files)
            {
                // removing the location from the filename
                string _name = (file.Replace(__path, ""));

                // removing the extention text from the file name
                _name = System.IO.Path.GetFileNameWithoutExtension(_name);

                // getting extention enum from extention text
                Extention _extention = ((new FileInfo(file)).Extension).Replace(".", "").ToExtentionEnum();

                // creating a blueprint for this existing file.
                var _file = new FileBlueprint(_name, _extention);

                // saying to the algorithm that the file is already built.
                _file.IsBuilt = true;

                // adding it to the list of this container.
                Add(_file);
            }
        }
        private void CheckForContentExistence_Folders(string __path)
        {
            var folders = Directory.GetDirectories(__path);
            foreach (string folderName in folders)
            {
                // creating a blueprint for this existing folder
                var _folder = new FolderBlueprint(folderName);

                // saying to the algorithm that this folder is already built.
                _folder.IsBuilt = true;

                // adding it to the list of this container.
                Add(_folder);
            }
        }
        private void CheckForContentExistence(string _path)
        {
            //checking if there is any content inside this previus existing directory
            try
            {
                CheckForContentExistence_Folders(_path);
                CheckForContentExistence_Files(_path);
            }
            catch
            {
                // 
            }
            finally
            {
                //
            }
        }
        internal override void CheckForExistence()
        {
            string _path = $"{Path}/";
            CheckForSelfExistence(_path);
            CheckForContentExistence(_path);
        }
        protected void ConstructorForFolder(string name)
        {
            Name = name;
            unbuildName = name;
        }
        protected void ConstructorForContainer(string name, string location)
        {
            ConstructorForFolder(name);
            Location = location;
            unbuildLocation = location;
            CheckForExistence();
        }
        protected void BuildAllContent()
        {
            BuildFolders();
            BuildFiles();
        }
        protected void UnbuildAllContent()
        {
            UnbuildFolders();
            UnbuildFiles();
        }
        private void BuildFiles()
        {
            if (ChildFileList.Count < 1) return;
            foreach (Files file in ChildFileList) file.Build();
        }
        private void UnbuildFiles()
        {
            if (ChildFileList.Count < 1) return;
            foreach (Files file in ChildFileList) file.Unbuild();
        }
        private void BuildFolders()
        {
            if (ChildFolderList.Count < 1) return;
            foreach (Folders folder in ChildFolderList) folder.Build();
        }
        private void UnbuildFolders()
        {
            if (ChildFolderList.Count < 1) return;
            foreach (Folders folder in ChildFolderList) folder.Unbuild();
        }
      

        #endregion --------------------------------------------
    }
}
