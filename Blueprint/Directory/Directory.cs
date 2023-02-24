using System.Collections.Generic;

namespace RepoBuilder
{
    public abstract class Directory : Blueprint
    {






        #region ===========- PRIVATE FIELDS -===================================================
        private List<Blueprint> contentToBuild = new List<Blueprint>();
        private List<Blueprint> contentToUnbuild = new List<Blueprint>();
        #endregion _____________________________________________________________________________







        #region ===========- PROTECTED PROPERTIES -=============================================
        protected override string UnbuildPath => $"{unbuildLocation}/{unbuildName}";
        #endregion _____________________________________________________________________________









        #region ===========- PRIVATE PROPERTIES -===============================================
        private DirectoryHelper Helper => new DirectoryHelper(this);
        #endregion _____________________________________________________________________________









        #region ===========- PUBLIC PROPERTIES -=================================================

        public override string Path => $"{Location}/{Name}";

        /// <summary> List of child document blueprints. </summary>
        public List<DocumentBlueprint> DocumentList => ContentList.Filter<DocumentBlueprint>();

        /// <summary> List of child folder blueprints. </summary>
        public List<FolderBlueprint> FolderList => ContentList.Filter<FolderBlueprint>();

        /// <summary> List of every content blueprint inside it. </summary>
        public List<Blueprint> ContentList => contentToBuild;
        #endregion _______________________________________________________________________________









        #region ===========- PUBLIC METHODS -=====================================================
        /// <summary>
        /// Clears the child list (files and folders).
        /// They will all be deleted once the Root calls the Build method again.
        /// </summary>
        public void Clear() {
            
            // unstage all content so they will be unbuilded on the next Build().
            contentToBuild.ForEach( item => contentToUnbuild.Add(item) );
            
            // clear all staged content so they won't be builded on the next Build().
            contentToBuild.Clear();
        }

        /// <summary> Adds blueprint into child list. </summary>
        public void Add(Blueprint content)
        {
            // if it is in the list, does nothing.
            if (ContentList.Contains(content)) return;

            // if it is a document...
            if(content is DocumentBlueprint) {
                
                var docBlueprint = (content as DocumentBlueprint);
                
                // compare to the existing ones,
                foreach (var existingItem in DocumentList)
                {
                    bool sameName = existingItem.Name == docBlueprint.Name;
                    bool sameExtention = existingItem.Extention == docBlueprint.Extention;
                    bool sameContent = existingItem.Content == docBlueprint.Content;
                    
                    // and if maches the same assignature, check for content.
                    if (sameName && sameExtention) {
                        if (sameContent) return;
                        // if they have different content written inside, opt for the newer one (just added).
                        Remove(existingItem);
                    }
                }
            }
            
            // if it is a folder...
            else if (content is FolderBlueprint)
            {
                // compare to the existing ones,
                foreach (var item in FolderList)
                {
                    bool sameName = item.Name == content.Name;
                    // and if maches the same assignature, do nothing.
                    if (sameName) return;
                }
            }

            // does not add Root since it must not be a child.
            if (content is RootBlueprint) return;

            // otherwise, add to the list.
            StageToBuild(content);

            // then, see if this blueprint points to an existing content.
            content.CheckIfPointsToExistingContent();
        }

        /// <summary> Removes blueprint from child list. </summary>
        public void Remove(Blueprint content)
        {
            // if it is not in the list, does nothing.
            if (!ContentList.Contains(content)) return;

            // does not remove Root since it is never added anyways.
            if (content is RootBlueprint) return;

            // otherwise, remove from the list.
            StageToUnbuild(content);
        }
        #endregion _______________________________________________________________________________









        #region ===========- INTERNAL METHODS -===================================================
        internal override void CheckIfPointsToExistingContent()
        {
            //string _path = $"{Path}/";
            string _path = Path;

            // if this Root blueprint does not point to an existent directory, does nothing.
            if (!System.IO.Directory.Exists(_path)) return;

            // otherwise, tell the algorithm that the Root is already built,
            IsBuilt = true;

            // can only be added into list if is not Root directory, but a Folder directory.
            if (this is FolderBlueprint)
            { 
                // create a blueprint for it,
                var newBlueprint = new FolderBlueprint(Name);
                // and add it to the parent list.
                DirectoryParent.Add(newBlueprint);
            }
            
            // now, check if there is any real content inside it to create and add blueprints inside its list.
            Helper.CheckForContentExistence(_path);
        }
        #endregion _______________________________________________________________________________










        #region ===========- PROTECTED METHODS -==================================================
        protected override void OnBuild()
        {
            System.IO.Directory.CreateDirectory(Path);

            // deleting child content that were removed from list.
            contentToUnbuild.ForEach(item => item.Unbuild());

            // building child content that were added to list.
            contentToBuild.ForEach(item => item.Build());
        }

        //protected override void OnUnbuild() => System.IO.Directory.Delete(UnbuildPath, true);


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
            CheckIfPointsToExistingContent();
        }
        #endregion _______________________________________________________________________________









        #region ===========- PRIVATE METHODS -==================================================
        private void StageToBuild(Blueprint content)
        {
            content.DirectoryParent = this;
            contentToBuild.Add(content);
            contentToUnbuild.Remove(content);
        }
        private void StageToUnbuild(Blueprint content)
        {
            content.DirectoryParent = null;
            contentToBuild.Remove(content);
            contentToUnbuild.Add(content);
        }
        #endregion _______________________________________________________________________________

    }
}
