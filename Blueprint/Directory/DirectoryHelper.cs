using System.IO;

namespace FileBuilder
{
    // internal struct DirectoryHelpers -> reference 'bug' since struct is a value type.
    internal class DirectoryHelper
    {
    // using class then to change the original Caller.




        #region ===========- PRIVATE FIELDS -=====================================================
        /// <summary> The directory blueprint that called for help. </summary>
        private Directory Caller;
        #endregion _______________________________________________________________________________








        #region ===========- CONSTRUCTOR -========================================================
        /// <summary> Creates a helper object with useful functions. </summary>
        /// <param name="caller">The directory blueprint that called for help.</param>
        internal DirectoryHelper(Directory caller) => Caller = caller;
        #endregion _______________________________________________________________________________









        #region ===========- BUILD METHODS -======================================================
        /// <summary> Creates or updates the real content inside it. </summary>
        internal void BuildAllContent()
        {
            BuildFolders();
            BuildFiles();
        }

        /// <summary> Deletes the real content inside it. </summary>
        internal void UnbuildAllContent()
        {
            UnbuildFolders();
            UnbuildFiles();
        }

        private void BuildFiles()
        {
            // there is not a single item in the list? do nothing.
            if (Caller.DocumentList.Count < 1) return;

            // otherwise, build all the files inside it.
            foreach (DocumentBlueprint doc in Caller.DocumentList) doc.Build();
        }

        private void UnbuildFiles()
        {
            // there is not a single item in the list? do nothing.
            if (Caller.DocumentList.Count < 1) return;

            // otherwise, unbuild all the files inside it.
            foreach (DocumentBlueprint doc in Caller.DocumentList) doc.Unbuild();
        }
        
        private void BuildFolders()
        {
            // there is not a single item in the list? do nothing.
            if (Caller.FolderList.Count == 0) return;

            // otherwise, build all the folders inside it.
            foreach (FolderBlueprint folder in Caller.FolderList) folder.Build();
        }

        private void UnbuildFolders()
        {
            // there is not a single item in the list? do nothing.
            if (Caller.FolderList.Count == 0) return;
            
            // otherwise, unbuild all the folders inside the list.
            foreach (FolderBlueprint folder in Caller.FolderList) folder.Unbuild();
        }
        #endregion _______________________________________________________________________________








        #region ===========- CHILD-LIST/CONTENT METHODS -=========================================

        /// <summary> Adds a file inside the child list of this directory. </summary>
        /// <param name="documentBlueprint">The file blueprint you want to add.</param>
        internal void AddFile(DocumentBlueprint documentBlueprint)
        {
            // same blueprint than any in list? then don't add.
            if (Caller.DocumentList.Contains(documentBlueprint)) return;

            // same assignature than any into list? then don't add.
            foreach (var item in Caller.DocumentList)
            {
                if (item.Name == documentBlueprint.Name && item.Extention == documentBlueprint.Extention)
                {
                    return;
                }
            }

            // ok, you can add.
            documentBlueprint.DirectoryParent = Caller;
            documentBlueprint.CheckForExistence();
            Caller.DocumentList.Add(documentBlueprint);
        }

        /// <summary> Removes a file from the child list of this directory. </summary>
        /// <param name="documentBlueprint">The file blueprint you want to remove.</param>
        internal void RemoveFile(DocumentBlueprint documentBlueprint)
        {
            // if it is not in the list, does nothing.
            if (!Caller.DocumentList.Contains(documentBlueprint)) return;

            // removes from the list.
            documentBlueprint.DirectoryParent = null;
            Caller.DocumentList.Remove(documentBlueprint);
            documentBlueprint.IsBuilt = false;
        }

        /// <summary> Adds a folder inside the child list of this directory. </summary>
        /// <param name="folderBlueprint">The folder blueprint you want to add.</param>
        internal void AddFolder(FolderBlueprint folderBlueprint)
        
        {
            // same blueprint than any in list? then don't add.
            if (Caller.FolderList.Contains(folderBlueprint)) return;

            // same assignature than any in list? then don't add.
            foreach (var item in Caller.FolderList)
            {
                if (item.Name == folderBlueprint.Name) return;
            }

            // ok, you can add.
            folderBlueprint.DirectoryParent = Caller;
            folderBlueprint.CheckForExistence();
            Caller.FolderList.Add(folderBlueprint);
        }

        /// <summary> Removes a folder from the child list of this directory. </summary>
        /// <param name="documentBlueprint">The folder blueprint you want to remove.</param>
        internal void RemoveFolder(FolderBlueprint folderBlueprint)
        {
            // if it is not in the list, does nothing.
            if (!Caller.FolderList.Contains(folderBlueprint)) return;

            // removes from the list.
            folderBlueprint.DirectoryParent = null;
            Caller.FolderList.Remove(folderBlueprint);
            Caller.DirectoryParent.IsBuilt = false;
        }
        #endregion _______________________________________________________________________________









        #region ===========- PREVIOUS EXISTENCE METHODS -=========================================
        /// <summary> Specifies if the this directory blueprint points to an existing directory. </summary>
        /// <param name="_path">Sanitized directory path</param>
        /// <returns>True if blueprint points to an existing directory.</returns>
        internal bool CheckForSelfExistence(string _path)
        {
            // if this Root blueprint does not point to an existent directory, returns false.
            if (!System.IO.Directory.Exists(_path)) return false;

            // otherwise, tell the algorithm that the Root is already built...
            Caller.IsBuilt = true;

            // ...and return true.
            return true;
        }

        /// <summary>
        /// Creates or updates the contents' blueprints list inside it,
        /// based on the previous existing files or folders on this path.
        /// </summary>
        /// <param name="_path">Sanitized directory path</param>
        internal void CheckForContentExistence(string _path)
        {
            // check if there is any content inside this previus existing directory.
            CheckForContentExistence_Folders(_path);
            CheckForContentExistence_Documents(_path);
        }

        private void CheckForContentExistence_Documents(string __path)
        {
            try
            {
                // get the docs' paths array from the this directory,
                string[] documentsPaths = System.IO.Directory.GetFiles(__path);

                // then loop throught this array for each path string,
                foreach (string docPath in documentsPaths)
                {
                    // save the name, by removing the location string before it,
                    string nameWithExtention = (docPath.Replace(__path, ""));

                    // remove the extention text from the file name,
                    string _name = Path.GetFileNameWithoutExtension(nameWithExtention);

                    // get the extention enum value from extention text and save it,
                    Extention _extention = ((new FileInfo(docPath)).Extension).Replace(".", "").ToExtentionEnum();

                    // create a blueprint for this existing document using the current _name and _extention fields from the loop,
                    var doc = new DocumentBlueprint(_name, _extention);

                    // tell the algorithm that this file blueprint is already built,
                    doc.IsBuilt = true;

                    // add it to the list of its parent.
                    Caller.Add(doc);
                }
            }
            finally
            {
                //
            }

        }
        private void CheckForContentExistence_Folders(string __path)
        {
            try
            {
                // get the folders' paths array from the this directory,
                string[] folderPaths = System.IO.Directory.GetDirectories(__path);

                // then loop throught this array for each path string,
                foreach (string folderPath in folderPaths)
                {
                    // create a name for it,
                    string _name = folderPath.Replace(__path, "");
                    
                    // create a blueprint for this existing folder using the current name from the loop,
                    var _folder = new FolderBlueprint(_name);

                    // then tell the algorithm that this folder blueprint is already built,
                    _folder.IsBuilt = true;

                    // and add it to this Root's list.
                    Caller.Add(_folder);
                }
            }
            finally
            {
                //
            }
        }

  
        #endregion _______________________________________________________________________________


    }
}
