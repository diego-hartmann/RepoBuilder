namespace RepoBuilder
{
    public class FolderBlueprint : Directory
    {
        
        #region ===========- CONSTRUCTORS -==================================
        public FolderBlueprint(string name) => ConstructorForFolder(name);
        #endregion __________________________________________________________








        #region ===========- PROTECTED METHODS -==============================================
        protected override void OnUnbuild()
        {
            foreach (var content in ContentList) content.Unbuild();
        }
        #endregion ___________________________________________________________________________
    }
}
