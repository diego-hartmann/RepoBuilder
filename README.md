# FileBuilder [(Download)](https://github.com/dieg0hartmann/FileBuilder/blob/main/bin/Debug/FileBuilder.dll)
1. C# library for creating files with a high level of abstraction and security.
2. Use simple methods to build, unbuild or rebuild your files.
3. Work with files without changing them directly everytime you edit some property, such as name or inner content.
4. Just change the blueprint object properties as many times as needed, then build all the changes at once.

## Import the namespace
So you will be able to use the types.
```cs
using FileBuilder;
```

## FileBlueprint type
The blueprint is just an object containing data and methods to create or update a real file. 

- Create the blueprint of the file using the FileBlueprint constructor. 
```cs
FileBlueprint fileBP = new FileBlueprint("c:/users/username/desktop", "MyFileName", Extention.JavaScript);
```
- You can pass a Folder enum value as the first parameter. It will be converted to string later.
```cs
FileBlueprint fileBP = new FileBlueprint(Folder.Desktop, "MyFileName", Extention.JavaScript);
```

## Blueprint object members
Modify the blueprint object using its methods and public properties.
```cs
fileBP.AddContentLine("const app = data => console.log(data);");
```
```cs
fileBP.AddContentLine("app();");
``` 
```cs
fileBP.Location = "c:/users/username/favourites";
```
```cs
fileBP.Location = Folder.Favorites.ToLocationString();
```

## Creating new file
You mount the new file (or rewrite the existing one) through the Build method.
```cs
fileBP.Build();
```

## Updating existing file
Just call the Build method whenever you want within your code.
- It will save the new changes into the existing file or create it anyways.
```cs
fileBP.ClearContent();
fileBP.Build();
```

## Deleting existing file
You unmount the real file through the Unbuild method.
- It DOES NOT delete the blueprint object.
- The existence of the real file is checked inside Unbuild method... 
- ... so don't worry trying to unbuild an inexisting file.
```cs
fileBP.Unbuild();
```