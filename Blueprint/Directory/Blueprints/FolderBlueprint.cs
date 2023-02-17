using System.Text;

namespace FileBuilder
{
    public class FolderBlueprint : Directory
    {
        
        #region ===========- CONSTRUCTORS -==================================
        public FolderBlueprint(string name) => ConstructorForFolder(name);
        #endregion __________________________________________________________

        
        #region ===========- PUBLIC PROPARTIES -=============================
        public override string Location => FolderParent?.Path ?? null;
        #endregion __________________________________________________________

    }
}
