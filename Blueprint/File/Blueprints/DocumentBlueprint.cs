namespace RepoBuilder
{
    public class DocumentBlueprint : File
    {



        #region ===========- CONSTRUCTORS -===============================================================
        /// <summary> Creates a virtual file that can be edited before being really created into your local machine. </summary>
        /// <param name="location">The path the real file will be created in.</param>
        /// <param name="name">The name the real file will have.</param>
        /// <param name="extention">The extention of the file.</param>
        public DocumentBlueprint(string name, Extention extention) => ConstructorForFile(name, extention);
        #endregion _______________________________________________________________________________________







        #region ===========- PUBLIC METHODS -===============================================================
        /// <summary> Creates another document blueprint based on this. </summary>
        /// <returns>Returns a new object with the same properties.</returns>
        public DocumentBlueprint GetCopy()
        {
            NumberOfCopies++;
            string newName = $"{Name}_{NumberOfCopies}";
            DocumentBlueprint duplication = new DocumentBlueprint(newName, Extention);
            duplication.Write(Content);
            return duplication;
        }
        #endregion _______________________________________________________________________________________
    }
}
