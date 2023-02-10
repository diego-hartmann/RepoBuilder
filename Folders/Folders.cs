using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileBuilder
{

    public abstract class Folders
    {

        #region ============ PRIVATE FIELDS ================
        private bool IsContainer = false;
        #endregion -----------------------------------------


        #region =========== PROTECTED FIELDS ====================
        protected string location = String.Empty;

        protected string unbuildLocation = String.Empty;
        protected string unbuildName = String.Empty;
        protected string unbuildLPath => $"{unbuildLocation}/{unbuildName}";
        #endregion --------------------------------------------



        #region =========== PUBLIC PROPERTIES =================
        public bool IsBuilt { get; protected set; }
        public string Name { get; protected set; }


        public Folders FolderParent { get; protected set; } = null;
        public string Location
        {
            get => location;
            protected set
            {
                var _value = new StringBuilder(value);
                _value.Replace("\\", "/");
                _value.Replace("//", "/");
                location = _value.ToString();
            }
        }

        public string Path => $"{Location}/{Name}";
        public List<Files> ChildFileList { get; protected set; } = new List<Files>();
        public List<Folders> ChildFolderList { get; protected set; } = new List<Folders>();
        #endregion --------------------------------------------



        #region =========== PUBLIC METHODS ====================
        public void Clear()
        {
            ChildFileList.Clear();
            ChildFolderList.Clear();
        }
        public Folders Add(Files fileBlueprint)
        {
            if (!ChildFileList.Contains(fileBlueprint))
            {
                fileBlueprint.checkIfFileExists();
                fileBlueprint.FolderParent = this;
                ChildFileList.Add(fileBlueprint);
            }
            return this;
        }
        public Folders Remove(Files fileBlueprint)
        {
            if (ChildFileList.Contains(fileBlueprint))
            {
                fileBlueprint.FolderParent = null;
                ChildFileList.Remove(fileBlueprint);
            }
            return this;
        }
        public Folders Add(Folders folderBlueprint)
        {
            if (!ChildFolderList.Contains(folderBlueprint))
            {
                folderBlueprint.Location = Path;
                folderBlueprint.FolderParent = this;
                folderBlueprint.CheckForExistingContent();
                ChildFolderList.Add(folderBlueprint);
            }
            return this;
        }
        public Folders Remove(Folders folderBlueprint)
        {
            if (ChildFolderList.Contains(folderBlueprint))
            {
                folderBlueprint.FolderParent = null;
                ChildFolderList.Remove(folderBlueprint);
            }
            return this;
        }
       

        #endregion --------------------------------------------




        #region ========= INTERNAL METHODS =========================
        internal void Build()
        {
            if (Location == null) return;
            Unbuild();
            this.unbuildLocation = this.Location;
            this.unbuildName = this.Name;
            Directory.CreateDirectory(Path);
            BuildAllContent();
        }

        internal void Unbuild()
        {
            if (!IsBuilt) return;
            Directory.Delete(unbuildLPath, true);
            UnbuildAllContent();
            IsBuilt = false;
        }
        #endregion -------------------------------------------------




        #region =========== PROTECTED METHODS ==================
        protected void CheckForExistingContent()
        {
            IsBuilt = Directory.Exists(Path);
            try
            {
                var folders = Directory.GetDirectories(Path);
                foreach (string folderName in Directory.GetDirectories(Path))
                {
                    var _folder = new FolderBlueprint(folderName);
                    _folder.IsBuilt = true;
                    Add(_folder);
                }

                var files = Directory.GetFiles(Path);
                foreach (string fileName in Directory.GetFiles(Path))
                {
                    FileInfo fi = new FileInfo(fileName);
                    Extention _fileExtention = fi.Extension.ToExtentionEnum();
                    var _file = new FileBlueprint(fileName, _fileExtention);
                    if (_file.checkIfFileExists()) Add(_file);
                }
            }
            catch
            {
                // 
            }
            
        }
        protected void Constructor(string name, string location = null)
        {
            Location = location;
            Name = name;
            unbuildLocation = location;
            unbuildName = name;
            CheckForExistingContent();
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
            foreach (FileBlueprint file in ChildFileList) file.Build();
        }
        private void UnbuildFiles()
        {
            if (ChildFileList.Count < 1) return;
            foreach (FileBlueprint file in ChildFileList) file.Unbuild();
        }
        private void BuildFolders()
        {
            if (ChildFolderList.Count < 1) return;
            foreach (FolderBlueprint folder in ChildFolderList) folder.Build();
        }
        private void UnbuildFolders()
        {
            if (ChildFolderList.Count < 1) return;
            foreach (FolderBlueprint folder in ChildFolderList) folder.Unbuild();
        }
      

        #endregion --------------------------------------------
    }
}
