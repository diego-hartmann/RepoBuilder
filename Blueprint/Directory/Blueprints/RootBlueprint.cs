using System;
using System.Text;

namespace RepoBuilder
{

    /// <summary>
    /// The root directory. It contains the Build and Unbuild method by extention.
    /// Those methods will build or unbuild its content (files or subfolders).
    /// Can not be moved into another blueprint.
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






        #region =========================- PRIVATE FIELDS -========================================================
        private string location = String.Empty;
        #endregion ________________________________________________________________________________________________






        #region ===========- PUBLIC PROPERTIES -==============================================
        public override string Location
        {
            get => location;

            protected set
            {
                StringBuilder _value = new StringBuilder(value);
                _value.Replace("\\", "/");
                _value.Replace("//", "/");
                location = _value.ToString();
            }
        }
        #endregion ___________________________________________________________________________









        #region ===========- PUBLIC METHODS -=================================================
        /// <summary> Moves the Root into another location. </summary>
        /// <param name="location">The new location.</param>
        public void MoveTo(string location) => Location = location;
        public void MoveTo(Location location) => Location = location.ToLocationString();
        #endregion ___________________________________________________________________________








        #region ===========- PROTECTED METHODS -==============================================
        protected override void OnUnbuild() {
            System.IO.Directory.Delete(UnbuildPath, true);
            foreach (var content in ContentList) content.Unbuild();
        }
        #endregion ___________________________________________________________________________



    }
}
