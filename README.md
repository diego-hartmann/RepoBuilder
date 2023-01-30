# FileBuilder
C# library for creating files with a high level of abstraction and security. Use simple methods to build, unbuild or rebuild your files into directories.

[DOWNLOAD HERE](https://github.com/dieg0hartmann/FileBuilder/blob/main/bin/Debug/FileBuilder.dll) the .dll and import it into your projects!

```cs
// import the namespace
using FileBuilder;

static class Program {
    static void Main()
    {

        // Create the blueprint of the file using the FileBlueprint constructor.
        FileBlueprint fileBP = new FileBlueprint("c:/users/username/desktop", "MyFileName", Extention.JavaScript);
            
        
        // In the first parameter, you can pass a SpecialFolder enum preset value as well, instead of the path string. 
        FileBlueprint fileBP = new FileBlueprint(SpecialFolder.Desktop, "MyFileName", Extention.JavaScript);

        
        // Add content into your file.
        fileBP.AddContentLine("const app = data => console.log(data);");
        fileBP.AddContentLine("app();");

        
        // You can create a real file using the Build method from the blueprint object.
        fileBP.Build();

        
        // You can edit the blueprint after the real file has been builded anyways...
        fileBP.AddContentLine("const app = () => console.log('arquivo');");
        // ... and even change its path ...
        fileBP.Location = "c:/users/username/desktop/yourFolder";
        // ...because the Build method will rewrite the real file based on those changes.
        fileBP.Build();


        // You can delete the real file with the Unbuild method.
        fileBP.Unbuild();

        
        // But you will always have the file blueprint object to edit and build whenever you want [fileBP].

    }
}
```