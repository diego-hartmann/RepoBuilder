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
        protected string unbuildFileExtentionText;

        /// <summary> Path used in the Unbuild method to correctly unbuild the old version without creating a copy. </summary>
        protected string UnbuildPath => $"{unbuildLocation}/{unbuildName}.{unbuildFileExtentionText}";
        #endregion ________________________________________________________________________________________________

    






        #region =========================- PUBLIC BLUEPRINT PROPERTIES -===========================================
        /// <summary> Specifies if this content is currently mounted with the Build method (readonly). </summary>
        public bool IsBuilt { get; internal set; }

        /// <summary> Specifies if this file has folder parent. </summary>
        public bool HasFolderParent => FolderParent != null;

        /// <summary> The folder parent of the file. </summary>
        public Folders FolderParent { get; internal set; }

        /// <summary> Location of the file. </summary>
        public virtual string Location { get => FolderParent?.Path ?? null; protected set { } }

        /// <summary> Name of the file. </summary>
        public string Name { get; set; }

        /// <summary> Complete path of the file, including location, name and extention (readonly). </summary>
        public abstract string Path { get; }
        #endregion ________________________________________________________________________________________________

    







        #region =========================- INTEERNAL BLUEPRINT METHODS -===========================================
        /// <summary> Specifies if the real file or folder exists based on this directory's of file's Path readonly string. </summary>
        internal abstract void CheckForExistence();

        /// <summary> Event to be called when the container is about to build. </summary>
        internal abstract void OnBuild();
      
        /// <summary> Creates the real file. </summary>
        internal void Build()
        {
            unbuildLocation = Location;
            unbuildName = Name;
            OnBuild();
            IsBuilt = true;
        }

        /// <summary> Event to be called when the container is about to unbuild. </summary>
        internal abstract void OnUnbuild();

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
