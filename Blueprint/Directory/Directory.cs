using System.Collections.Generic;
using System.Text;

namespace RepoBuilder
{
    public abstract class Directory : Blueprint
    {







        #region ===========- PROTECTED PROPERTIES -=============================================
        protected override string UnbuildPath => $"{unbuildLocation}/{unbuildName}";
        #endregion _____________________________________________________________________________








        #region ===========- PRIVATE PROPERTIES -===============================================
        private DirectoryHelper Helper => new DirectoryHelper(this);
        #endregion _____________________________________________________________________________









        #region ===========- PUBLIC PROPERTIES -=================================================
        public override string Location
        {
            get => DirectoryParent?.Path ?? null;
            
            protected set
            {
                StringBuilder _value = new StringBuilder(value);
                _value.Replace("\\", "/");
                _value.Replace("//", "/");
                _value.Replace("/", "/");
                location = _value.ToString();
            }
        }

        public override string Path => $"{Location}/{Name}" ?? null;
        
        /// <summary> List of child files. </summary>
        public List<File> DocumentList { get; protected set; } = new List<File>();

        /// <summary> List of child folders. </summary>
        public List<Directory> FolderList { get; protected set; } = new List<Directory>();
        #endregion _______________________________________________________________________________









        #region ===========- PUBLIC METHODS -=====================================================
        /// <summary>
        /// Clears the child list (files and folders).
        /// They will all be deleted once the Root calls the Build method again.
        /// </summary>
        public void Clear()
        {
            DocumentList.Clear();
            FolderList.Clear();
        }

        /// <summary> Adds content into child list (files or folders). </summary>
        public void Add(Blueprint content)
        {
            // Does not add Root since it is already the root.
            if (content is RootBlueprint) return;

            // Adds document.
            if (content is DocumentBlueprint)
            {
                Helper.AddFile(content as DocumentBlueprint);
                return;
            }

            // Adds folder.
            Helper.AddFolder(content as FolderBlueprint);
        }

        /// <summary> Removes content from child list (files or folders). </summary>
        public void Remove(Blueprint content)
        {
            // Does not remove Root since it is never added anyways.
            if (content is RootBlueprint) return;

            // Removes document.
            if (content is DocumentBlueprint)
            {
                Helper.RemoveFile(content as DocumentBlueprint);
                return;
            }

            // Remove folder.
            Helper.RemoveFolder(content as FolderBlueprint);
        }
        #endregion _______________________________________________________________________________









        #region ===========- INTERNAL METHODS -===================================================
        internal override void CheckForExistence()
        {
            string _path = $"{Path}/";

            // if the Root does not exist, don't do anything.
            if (!Helper.CheckForSelfExistence(_path)) return;

            // otherwise, check if its content exists too.
            Helper.CheckForContentExistence(_path);
        }
        #endregion _______________________________________________________________________________










        #region ===========- PROTECTED METHODS -==================================================
        protected override void OnBuild()
        {
            System.IO.Directory.CreateDirectory(Path);
            Helper.BuildAllContent();
        }

        protected override void OnUnbuild()
        {
            System.IO.Directory.Delete(UnbuildPath, true);
            foreach (var item in DocumentList) item.IsBuilt = false;
            foreach (var item in FolderList) item.IsBuilt = false;
        }

        protected void ConstructorForFolder(string name)
        {
            Name = name;
            unbuildName = name;
        }

        protected void ConstructorForRoot(string name, string location)
        {
            ConstructorForFolder(name);
            Location = location;
            unbuildLocation = location;
            CheckForExistence();
        }
        #endregion _______________________________________________________________________________
    }
}
