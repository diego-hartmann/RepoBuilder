using System.IO;

namespace FileBuilder
{
    public class FileBlueprint : Files
    {

        #region =========== CONSTRUCTORS ===============================================================

        /// <summary> Creates a virtual file that can be edited before being really created into your local machine. </summary>
        /// <param name="location">The path the real file will be created in.</param>
        /// <param name="name">The name the real file will have.</param>
        /// <param name="extention">The extention of the file.</param>
        public FileBlueprint(string name, Extention extention) => Constructor(name, extention);

        #endregion =======================================================================================



        #region =========== PUBLIC METHODS ===============================================================
        /// <summary> Creates another blueprint based on this. </summary>
        /// <returns>Returns a new object with the same properties.</returns>
        public FileBlueprint GetCopy()
        {
            NumberOfCopies++;
            string newName = $"{Name}_{NumberOfCopies}";
            FileBlueprint duplication = new FileBlueprint(newName, Extention);
            duplication.AddContent(Content);
            return duplication;
        }


        public void MoveTo(Folders folder) => folder.Add(this);

        #endregion =======================================================================================

    }
}
