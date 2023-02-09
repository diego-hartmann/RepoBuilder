using System;
using System.IO;
using System.Collections.Generic;

namespace FileBuilder
{
    public class FolderBlueprint
    {


        #region =========== CONSTRUCTORS ======================
        public FolderBlueprint(string name, string location) => Constructor(name, location);
        public FolderBlueprint(string name, Location location) => Constructor(name, location.ToLocationString());
        public FolderBlueprint(string name) => Constructor(name, null);
        #endregion --------------------------------------------
        
        
        
        
        #region =========== PRIVATE FIELDS ====================
        private string location = String.Empty;

        private string unbuildLocation = String.Empty;
        private string unbuildName = String.Empty;
        private string unbuildLPath => $"{unbuildLocation}/{unbuildName}";
        #endregion --------------------------------------------




        #region =========== PUBLIC PROPERTIES =================
        public bool IsBuilt { get; private set; }
        public string Name { get; private set; }


        public bool HasFolderParent => FolderParent != null;
        public FolderBlueprint FolderParent { get; internal set; } = null;
        public string Location {
            get
            {
                if (HasFolderParent) return FolderParent.Path;
                else return location;
            }
            set {
                if (value != null) location = value.Replace("\\", "/");
                else location = value;
                FolderParent?.Remove(this);
            }
        }

        public string Path => $"{Location}/{Name}";
        public List<FileBlueprint> ChildFileList { get; private set; } = new List<FileBlueprint>();
        public List<FolderBlueprint> ChildFolderList { get; private set; } = new List<FolderBlueprint>();
        #endregion --------------------------------------------




        #region =========== PUBLIC METHODS ====================

        public void Clear()
        {
            ChildFileList.Clear();
            ChildFolderList.Clear();
        }
        public void Add(FileBlueprint fileBlueprint)
        {
            if (!ChildFileList.Contains(fileBlueprint)) {
                fileBlueprint.FolderParent = this;
                ChildFileList.Add(fileBlueprint);
            }
        }
        public void Remove(FileBlueprint fileBlueprint)
        {
            if (ChildFileList.Contains(fileBlueprint)) { 
                fileBlueprint.FolderParent = null;
                ChildFileList.Remove(fileBlueprint);
            }
        }
        public void Add(FolderBlueprint folderBlueprint)
        {
            if (!ChildFolderList.Contains(folderBlueprint)) {
                folderBlueprint.Location = Path;
                folderBlueprint.FolderParent= this;
                ChildFolderList.Add(folderBlueprint);
            }
        }
        public void Remove(FolderBlueprint folderBlueprint)
        {
            if (ChildFolderList.Contains(folderBlueprint))
            {
                folderBlueprint.FolderParent = null;
                ChildFolderList.Remove(folderBlueprint);
            }
        }

        public void MoveTo(FolderBlueprint parent) {
            FolderParent?.Remove(this);
            FolderParent = parent;
            FolderParent.Add(this);
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




        #region =========== PRIVATE METHODS ==================
        public void Constructor(string name, string location)
        {
            this.Location = location;
            this.Name = name;
            this.unbuildLocation = location;
            this.unbuildName = name;

            IsBuilt = Directory.Exists(Path);
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
        private void BuildAllContent()
        {
            BuildFolders();
            BuildFiles();
        }
        private void UnbuildAllContent()
        {
            UnbuildFolders();
            UnbuildFiles();
        }
        #endregion --------------------------------------------


    }
}
