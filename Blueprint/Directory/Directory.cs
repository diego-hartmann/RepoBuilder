using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace RepoBuilder
{
    public abstract class Directory : Blueprint
    {






        #region ===========- PRIVATE FIELDS -===================================================
        //private List<DocumentBlueprint> documentsToBuild = new List<DocumentBlueprint>();
        //private List<DocumentBlueprint> documentsToDelete = new List<DocumentBlueprint>();

        //private List<FolderBlueprint> foldersToBuild = new List<FolderBlueprint>();
        //private List<FolderBlueprint> foldersToDelete = new List<FolderBlueprint>();
        private List<Blueprint> contentToBuild = new List<Blueprint>();
        private List<Blueprint> contentToUnbuild = new List<Blueprint>();
        #endregion _____________________________________________________________________________







        #region ===========- PROTECTED PROPERTIES -=============================================
        protected override string UnbuildPath => $"{unbuildLocation}/{unbuildName}";
        #endregion _____________________________________________________________________________








        #region ===========- PRIVATE METHODS -==================================================
        
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
        
        /// <summary> List of child document blueprints. </summary>
        public List<DocumentBlueprint> DocumentList
        {
            get
            {
                var docList = new List<DocumentBlueprint>();

                contentToBuild.ForEach(item =>
                {
                    if (item is DocumentBlueprint) docList.Add(item as DocumentBlueprint);
                });
                return docList;
            }
        }

        /// <summary> List of child folder blueprints. </summary>
        public List<FolderBlueprint> FolderList
        {
            get
            {
                var folderList = new List<FolderBlueprint>();

                contentToBuild.ForEach(item =>
                {
                    if (item is FolderBlueprint) folderList.Add(item as FolderBlueprint);
                });
                return folderList;
            }
        }

        /// <summary> List of every content blueprint inside it. </summary>
        public List<Blueprint> ContentList => contentToBuild;


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

        /// <summary> Adds blueprint into child list. </summary>
        public void Add(Blueprint content)
        {
            // if it is in the list, does nothing.
            if (ContentList.Contains(content)) return;

            // does not add Root since it is never a child.
            if (content is RootBlueprint) return;

            // otherwise, add to the list.
            content.DirectoryParent = null;
            contentToBuild.Remove(content);
            contentToUnbuild.Add(content);
        }

        /// <summary> Removes blueprint from child list. </summary>
        public void Remove(Blueprint content)
        {
            // if it is not in the list, does nothing.
            if (!ContentList.Contains(content)) return;

            // does not remove Root since it is never added anyways.
            if (content is RootBlueprint) return;

            // otherwise, remove from the list.
            content.DirectoryParent = null;
            contentToBuild.Remove(content);
            contentToUnbuild.Add(content);
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

            // updates the existence of its child blueprint content list.
            contentToBuild.ForEach(item => item.Build());
            contentToUnbuild.ForEach(item => item.Unbuild());
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
