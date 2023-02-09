using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileBuilder
{

    public abstract class Folder
    {
        public static class Create
        {
            public static Container Container(string name, string location) => new Container(name, location);
            public static Container Container(string name, Location location) => new Container(name, location.ToLocationString());
            public static FolderBlueprint Blueprint(string name, string location) => new FolderBlueprint(name, location);
            public static FolderBlueprint Blueprint(string name, Location location) => new FolderBlueprint(name, location.ToLocationString());
            public static FolderBlueprint Blueprint(string name) => new FolderBlueprint(name);
        }

        
        #region =========== PROTECTED FIELDS ====================
        protected string location = String.Empty;

        protected string unbuildLocation = String.Empty;
        protected string unbuildName = String.Empty;
        protected string unbuildLPath => $"{unbuildLocation}/{unbuildName}";
        #endregion --------------------------------------------




        #region ========== PROTECTED METHODS =====================
        protected void Constructor(string name, string location)
        {
            Location = location;
            Name = name;
            unbuildLocation = location;
            unbuildName = name;
            IsBuilt = Directory.Exists(Path);
        }
        #endregion ------------------------------------------------------


       

        #region =========== PUBLIC PROPERTIES =================
        public bool IsBuilt { get; protected set; }
        public string Name { get; protected set; }


        public Folder FolderParent { get; protected set; } = null;
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
        public List<DocumentBlueprint> ChildFileList { get; protected set; } = new List<DocumentBlueprint>();
        public List<Folder> ChildFolderList { get; protected set; } = new List<Folder>();
        #endregion --------------------------------------------



        #region =========== PUBLIC METHODS ====================
        public void Clear()
        {
            ChildFileList.Clear();
            ChildFolderList.Clear();
        }
        public void Add(DocumentBlueprint fileBlueprint)
        {
            if (!ChildFileList.Contains(fileBlueprint))
            {
                fileBlueprint.FolderParent = this;
                ChildFileList.Add(fileBlueprint);
            }
        }
        public void Remove(DocumentBlueprint fileBlueprint)
        {
            if (ChildFileList.Contains(fileBlueprint))
            {
                fileBlueprint.FolderParent = null;
                ChildFileList.Remove(fileBlueprint);
            }
        }
        public void Add(Folder folderBlueprint)
        {
            if (!ChildFolderList.Contains(folderBlueprint))
            {
                folderBlueprint.Location = Path;
                folderBlueprint.FolderParent = this;
                ChildFolderList.Add(folderBlueprint);
            }
        }
        public void Remove(Folder folderBlueprint)
        {
            if (ChildFolderList.Contains(folderBlueprint))
            {
                folderBlueprint.FolderParent = null;
                ChildFolderList.Remove(folderBlueprint);
            }
        }
        public void Add(FolderBlueprint folderBlueprint)
        {
            if (!ChildFolderList.Contains(folderBlueprint))
            {
                folderBlueprint.Location = Path;
                folderBlueprint.FolderParent = this;
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
            foreach (DocumentBlueprint file in ChildFileList) file.Build();
        }
        private void UnbuildFiles()
        {
            if (ChildFileList.Count < 1) return;
            foreach (DocumentBlueprint file in ChildFileList) file.Unbuild();
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
