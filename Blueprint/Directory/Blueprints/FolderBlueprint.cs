namespace RepoBuilder
{
    public class FolderBlueprint : Directory
    {
        
        #region ===========- CONSTRUCTORS -==================================
        public FolderBlueprint(string name) => ConstructorForFolder(name);
        #endregion __________________________________________________________

        
        #region ===========- PUBLIC PROPARTIES -=============================
        public override string Location => DirectoryParent?.Path ?? null;
        #endregion __________________________________________________________

    }
}
