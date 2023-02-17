using System;
using System.Collections.Generic;
using System.Text;

namespace FileBuilder
{
    public abstract class Directory : Blueprint
    {





        #region ===========- PRIVATE FIELDS -===================================================
        private DirectoryHelpers helper;
        #endregion _____________________________________________________________________________









        #region ===========- PROTECTED FIELDS -=================================================
        protected override string UnbuildPath => $"{unbuildLocation}/{unbuildName}";
        #endregion _____________________________________________________________________________









        #region ===========- PUBLIC PROPERTIES -=================================================
        public override string Location
        {
            get => FolderParent?.Path ?? null;
            
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
        public List<File> ChildFileList { get; protected set; } = new List<File>();

        /// <summary> List of child folders. </summary>
        public List<Directory> ChildFolderList { get; protected set; } = new List<Directory>();
        #endregion _______________________________________________________________________________









        #region ===========- PUBLIC METHODS -=====================================================
        /// <summary>
        /// Clears the while child list (files and folders).
        /// They will all be deleted once the Root calls the Build method again.
        /// </summary>
        public void Clear()
        {
            ChildFileList.Clear();
            ChildFolderList.Clear();
        }

        /// <summary> Adds content into child list (files or folders). </summary>
        public void Add<T>(T content) where T : Blueprint
        {
            // Does not add Root since it is already the root.
            if (content is RootBlueprint) return;

            // Adds document.
            if (content is DocumentBlueprint)
            {
                helper.AddFile(content as DocumentBlueprint);
                return;
            }

            // Adds folder.
            helper.AddFolder(content as FolderBlueprint);
        }

        /// <summary> Removes content from child list (files or folders). </summary>
        public void Remove<T>(T content) where T : Blueprint
        {
            // Getting the type of the parameter.
            var _type = typeof(T);

            // Does not remove Root since it is never added.
            if (_type == typeof(RootBlueprint)) return;

            // Removes file.
            if (_type == typeof(DocumentBlueprint))
            {
                helper.RemoveFile(content as DocumentBlueprint);
                return;
            }

            // Removes folder.
            helper.RemoveFolder(content as FolderBlueprint);
        }
        #endregion _______________________________________________________________________________









        #region ===========- INTERNAL METHODS -===================================================
        internal override void CheckForExistence()
        {
            string _path = $"{Path}/";

            // if the Root does not exist, don't do anything.
            if (!helper.CheckForSelfExistence(_path)) return;

            // otherwise, check if its content exists too.
            helper.CheckForContentExistence(_path);
        }
        #endregion _______________________________________________________________________________









        #region ===========- PROTECTED METHODS -==================================================
        protected override void OnBuild()
        {
            System.IO.Directory.CreateDirectory(Path);
            helper.BuildAllContent();
        }

        protected override void OnUnbuild()
        {
            System.IO.Directory.Delete(UnbuildPath, true);
            helper.UnbuildAllContent();
        }

        protected void ConstructorForFolder(string name)
        {
            Name = name;
            unbuildName = name;
            helper = new DirectoryHelpers(this);
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
