using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileBuilder
{

    public abstract class Folders
    {



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
            // ok
            fileBlueprint.CheckForExistingFile();
            fileBlueprint.FolderParent = this;
            ChildFileList.Add(fileBlueprint);
        }
        public void Remove(Files fileBlueprint)
        {
            if (ChildFileList.Contains(fileBlueprint))
            {
                fileBlueprint.FolderParent = null;
                ChildFileList.Remove(fileBlueprint);
            }
        }
        public void Add(Folders folderBlueprint)
        {
            // same blueprint...
            if (ChildFolderList.Contains(folderBlueprint)) return;
            // same assignature...
            foreach (var item in ChildFolderList)
            {
                if (item.Name == folderBlueprint.Name)
                {
                    return;
                }
            }
            folderBlueprint.Location = Path;
            folderBlueprint.FolderParent = this;
            folderBlueprint.CheckForExistingContent();
            ChildFolderList.Add(folderBlueprint);
        }
        public void Remove(Folders folderBlueprint)
        {
            if (ChildFolderList.Contains(folderBlueprint))
            {
                folderBlueprint.FolderParent = null;
                ChildFolderList.Remove(folderBlueprint);
            }
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
            string _path = $"{Path}/";
            IsBuilt = Directory.Exists(_path);
            try
            {
                var folders = Directory.GetDirectories(_path);
                foreach (string folderName in folders)
                {
                    var _folder = new FolderBlueprint(folderName);
                    _folder.IsBuilt = true;
                    Add(_folder);
                }

                var files = Directory.GetFiles(_path);
                foreach (string file in files)
                {
                    // removing the location from the filename
                    string _name = (file.Replace(_path, ""));
                    
                    // removing the extention text from the file name
                    _name = System.IO.Path.GetFileNameWithoutExtension(_name);
                    
                    // getting extention enum from extention text
                    Extention _extention = ((new FileInfo(file)).Extension).Replace(".","").ToExtentionEnum();
                    
                    // adding file to the list of blueprints.
                    var _file = new FileBlueprint(_name, _extention);
                    Add(_file);
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
