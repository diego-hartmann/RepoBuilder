using System.IO;

namespace FileBuilder
{
    public class FolderBlueprint : Folder
    {

        #region =========== CONSTRUCTORS ======================
        internal FolderBlueprint(string name) => Constructor(name, null);
        internal FolderBlueprint(string name, string location) => Constructor(name, location);
        #endregion --------------------------------------------
        

        #region =========== PUBLIC PROPERTIES =================
        public bool HasFolderParent => FolderParent != null;
        #endregion --------------------------------------------


        #region =========== PUBLIC METHODS ====================
        public void MoveTo(Folder parent) {
            FolderParent?.Remove(this);
            FolderParent = parent;
            FolderParent.Add(this);
        }
        #endregion --------------------------------------------

    }
}
