using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace RepoBuilder
{
    internal class DirectoryHelper
    {




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
