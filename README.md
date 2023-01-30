# FileBuilder
C# library for creating files with a high level of abstraction and security. Use simple methods to build, unbuild or rebuild your files into directories.

[DOWNLOAD HERE](https://github.com/dieg0hartmann/FileBuilder/blob/main/bin/Debug/FileBuilder.dll) the .dll and import it into your projects!

```cs
// import the namespace
using FileBuilder;

static class Program {
    static void Main()
    {

        // ========= BLUEPRINT OBJECT =================================================================================================
        
        // --- The blueprint is just an object containing data and methos to create or update a real file. 
        // --- Create the blueprint of the file using the FileBlueprint constructor. 
        FileBlueprint fileBP = new FileBlueprint("c:/users/username/desktop", "MyFileName", Extention.JavaScript);
        // --- Or, in the first parameter, you can pass a Folder enum value, instead of the path string. It will be converted to string later.
        FileBlueprint fileBP = new FileBlueprint(Folder.Desktop, "MyFileName", Extention.JavaScript);



        // ========= MODIFYING BLUEPRINT ===============================================================================================
        
        // --- Modify the blueprint object using its methods and public properties.
        fileBP.AddContentLine("const app = data => console.log(data);");
        fileBP.AddContentLine("app();");
        fileBP.Location = Folder.Favorites.ToLocationString(); // or simple string -> "c:/users/username/favourites"



        // === MOUNTING THE REAL FILE ==================================================================================================
        
        // --- You create (or rewrite) the real file using the Build method. It saves the blueprint changes into the real file.
        fileBP.Build();
        // --- You delete the real file using the Unbuild method.
        fileBP.Unbuild();


        // === UPDATING REAL FILE ======================================================================================================

        // -- Just call the Build method whenever you want to, so it saves the new changes into the existing file.
        fileBP.ClearContent();
        fileBP.Build();

    }
}
```