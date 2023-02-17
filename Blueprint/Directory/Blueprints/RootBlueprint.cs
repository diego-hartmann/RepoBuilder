using System.Text;

namespace FileBuilder
{

    /// <summary>
    /// The root directory. It contains the Build and Unbuild method.
    /// Those methods will build or unbuild its content (files or subfolders).
    /// Can not be moved
    /// </summary>
    public class RootBlueprint : Directory
    {





        #region ===========- CONSTRUCTORS -===================================================
        /// <summary> Returns a new Root object. </summary>
        /// <param name="name">Name for the real directory. </param>
        /// <param name="location">Location string path that the root will be built in.</param>
        public RootBlueprint(string name, string location) => ConstructorForRoot(name, location);
        
        /// <summary> Returns a new Root object. </summary>
        /// <param name="name">Name for the real directory. </param>
        /// <param name="location">Location enum path that the Root will be built in.</param>
        public RootBlueprint(string name, Location location) => ConstructorForRoot(name, location.ToLocationString());
        #endregion ___________________________________________________________________________







        #region ===========- PUBLIC PROPERTIES -==============================================
        public override string Location
        {
            get => location;

            protected set
            {
                StringBuilder _value = new StringBuilder(value);
                _value.Replace("\\", "/");
                _value.Replace("//", "/");
                _value.Replace("/", "/");
                location = _value.ToString();
            }
        }
        #endregion ___________________________________________________________________________









        #region ===========- PUBLIC METHODS -=================================================
        /// <summary> Moves the directory into another one </summary>
        /// <param name="location"></param>
        public void MoveTo(string location) => Location = location;
        public void MoveTo(Location location) => Location = location.ToLocationString();
        #endregion ___________________________________________________________________________



    }
}
