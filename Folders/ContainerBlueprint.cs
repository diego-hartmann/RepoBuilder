using System.IO;

namespace FileBuilder
{

    /// <summary>
    /// The special folder that contains the Build and Unbuild method.
    /// Those methods will build or unbuild its content (files or subfolders).
    /// Can not be moved
    /// </summary>
    
    public class ContainerBlueprint : Folders
    {
        #region =========== CONSTRUCTORS ======================
        public ContainerBlueprint(string name, string location) => ConstructorForContainer(name, location);
        public ContainerBlueprint(string name, Location location) => ConstructorForContainer(name, location.ToLocationString());
        #endregion --------------------------------------------
        public void MoveTo(string location) => this.Location = location;
        public void MoveTo(Location location) => this.Location = location.ToLocationString();
    }
}
