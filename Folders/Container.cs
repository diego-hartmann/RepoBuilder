using System.IO;

namespace FileBuilder
{

    /// <summary>
    /// The special folder that contains the Build and Unbuild method.
    /// Those methods will build or unbuild its content (files or subfolders).
    /// Can not be moved
    /// </summary>
    
    public class Container : Folder
    {

        #region =========== CONSTRUCTORS ======================
        internal Container(string name, string location) => Constructor(name, location);
        #endregion --------------------------------------------
        public void MoveTo(string location) => this.Location = location;
    }
}
