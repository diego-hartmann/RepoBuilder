using System;
using System.IO;

namespace FileBuilder
{
    public abstract class Blueprint
    {
        #region
        // these three data hold the last saved information to correctly unbuild the old version of the file through its old Path (UnbuildPath bellow).
        // they will be updated into the Build method as the last path version saved.
        protected string unbuildLocation = String.Empty;
        protected string unbuildName = String.Empty;
        protected string unbuildFileExtentionText;

        // called on Unbuild method;
        protected string UnbuildPath => $"{unbuildLocation}/{unbuildName}.{unbuildFileExtentionText}";
        #endregion

        #region
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
        #endregion


        #region
        internal abstract void CheckForExistence();


        /// <summary> Creates the real file. </summary>
        /// <param name="fileBlueprint">The blueprint of the real file to be created.</param>
        internal abstract void OnBuild();
        /// <summary> Creates the real file. </summary>
        /// <param name="fileBlueprint">The blueprint of the real file to be created.</param>        
        internal void Build()
        {
            this.unbuildLocation = this.Location;
            this.unbuildName = this.Name;
            OnBuild();
            IsBuilt = true;
        }

        /// <summary> Deletes the real file. </summary>
        /// <param name="fileBlueprint">The blueprint of the real file to be deleted.</param>
        internal abstract void OnUnbuild();

        /// <summary> Deletes the real file. </summary>
        /// <param name="fileBlueprint">The blueprint of the real file to be deleted.</param>
        internal void Unbuild()
        {
            if (!IsBuilt) return;
            // using the update UnbuildPath to delete the last version of the file
            OnUnbuild();
            IsBuilt = false;
        }
        #endregion

    }

}
