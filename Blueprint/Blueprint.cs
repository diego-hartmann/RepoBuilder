using System;

namespace FileBuilder
{
    public abstract class Blueprint
    {


        #region =========================- PROTECTED UNBUILD DATA -================================================
        // these three data hold the last saved information to correctly unbuild the old version of the file through its old Path (UnbuildPath bellow).
        // they will be updated into the Build method as the last path version saved.
        protected string unbuildLocation = String.Empty;
        protected string unbuildName = String.Empty;
        protected string unbuildFileExtentionText = String.Empty;

        /// <summary> Path used in the Unbuild method to correctly unbuild the old version without creating a copy. </summary>
        protected abstract string UnbuildPath { get; }
        #endregion ________________________________________________________________________________________________









        #region =========================- PROTECTED PROPERTIES -==================================================
        protected string location = String.Empty;
        #endregion ________________________________________________________________________________________________









        #region =========================- PUBLIC GET{} PROPERTIES -===============================================
        /// <summary> Specifies if this content is currently mounted with the Build method. </summary>
        public bool IsBuilt { get; internal set; }

        /// <summary> Specifies if this file has folder parent. </summary>
        public bool HasFolderParent => FolderParent != null;

        /// <summary> The folder parent of the file. </summary>
        public Directory FolderParent { get; internal set; }

        /// <summary> Location of the file. </summary>
        public virtual string Location { get => FolderParent?.Path ?? null; protected set { } }

        /// <summary> Name of the file. </summary>
        public string Name { get; protected set; }

        /// <summary> Complete path of the file, including location, name and extention. </summary>
        public abstract string Path { get; }
        #endregion ________________________________________________________________________________________________









        #region =========================- PUBLIC METHODS -=======================================================
        /// <summary>
        /// Moves the file of folder to this parent directory.
        /// The next Build() will create it into the directory.
        /// </summary>
        /// <param name="folder">Directory to be the parent.</param>
        public void MoveTo(Directory folder) => folder.Add(this);

        /// <summary>
        /// Removes the file of folder from its parent directory.
        /// The next Build() will delete it from that directory.
        /// </summary>
        public void LeaveFolder() => FolderParent?.Remove(this);

        /// <summary> Changes the blueprint's Name property. </summary>
        /// <param name="newName">The new name you want to add to the blueprint.</param>
        public void Rename(string newName) => Name = newName;
        #endregion ________________________________________________________________________________________________









        #region =========================- PROTECTED METHODS -====================================================
        /// <summary> Event to be called when the Root is about to unbuild. </summary>
        protected abstract void OnUnbuild();

        /// <summary> Event to be called when the Root is about to build. </summary>
        protected abstract void OnBuild();
        #endregion ________________________________________________________________________________________________








        #region =========================- INTERNAL METHODS -=======================================================
        /// <summary> Specifies if the real file or folder exists based on this directory's of file's Path readonly string. </summary>
        internal abstract void CheckForExistence();
      
        /// <summary> Creates the real file. </summary>
        internal void Build()
        {
            unbuildLocation = Location;
            unbuildName = Name;
            OnBuild();
            IsBuilt = true;
        }

        /// <summary> Deletes the real file. </summary>
        internal void Unbuild()
        {
            if (!IsBuilt) return;
            // using the update UnbuildPath to delete the last version of the file
            OnUnbuild();
            IsBuilt = false;
        }
        #endregion ________________________________________________________________________________________________


    }
}
