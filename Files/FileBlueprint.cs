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
        public FileBlueprint(string name, Extention extention) => ConstructorForFile(name, extention);
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

        /// <summary>
        /// Moves the blueprint to the directory's child list.
        /// It can be the container or folders.
        /// The next Build() will create it into the directory.
        /// </summary>
        /// <param name="folder">Container of folder.</param>
        public void MoveTo(Folders folder) => folder.Add(this);

        /// <summary>
        /// Removes the file from its parent child list.
        /// The next Build() will delete it from the directory.
        /// </summary>
        public void ExitFolder() => FolderParent?.Remove(this);
        #endregion =======================================================================================

    }
}
