# RepoBuilder 📁
## 1. [Download](https://github.com/diego-hartmann/RepoBuilder/raw/main/bin/Debug/RepoBuilder.dll) the .dll
1. C# library for creating files with a high level of abstraction and security.
2. Use simple methods to build, unbuild or rebuild your repository (root directory with files).
3. Work with files and folders without changing them directly everytime you edit some property, such as name or inner content.
4. Just change the blueprint object properties as many times as needed, then build all the changes at once.

## 2. Import the namespace
So you will be able to use the types.
```cs
using RepoBuilder;
```

# Blueprint class
The Blueprint.cs is the base class for three objects containing data and methods to create or update their real content. 
It is a virtual file or folder that can be edited before being really created into your local machine.

There are 3 (three) types (sub-classes) derived from the Blueprint class, which are:

## 1. RootBlueprint type
- Creates the blueprint of the root directory using the RootBlueprint constructor. 
```cs
var root = new RootBlueprint("MyRepoName", "c:/users/{userName}/desktop");
```
- You can pass a Location enum value as the second parameter. It will be converted to string later.
```cs
var root = new RootBlueprint("MyRepoName", Location.Desktop);
```
- This is the only Blueprint that implements the extention methods Build() and Unbuild().
- The Build() method mounts the real repository into your local machine.
- It also mounts all of its inner contents (folders and documents blueprints added to its content list).
```cs
root.Build(); // creating
```
- The Unbuild method will unmount the real root directory from your local machine.
- The root object still exists and can be Builded again.
```cs
root.Unbuild(); // deleting
```
- The root object stills holds its theorical data and values,
- So it can be rebuilded as many time as you see fit within the code.
```cs
root.Unbuild(); // deleting
root.Build(); // recreating
```
- Building both creates or updates the folder or file for which the blueprint is pointing.
- A blueprint points to an existing file or folder if its assignature (name, path and content) matches any real file's assignature.
- The blueprint may be rebuilded as many time as you see fit within the code.
```cs
root.Build(); // creating or updating existing folder.
// some changes on root (it can be files added or removed, or name changed, etc. More exemples following).
root.Build(); // updating the folder with the new changes.
```

## 4. DocumentBlueprint object members
Modify the blueprint object using its methods and public properties.
```cs
fileBP.AddContentLine("const app = data => console.log(data);");
```
```cs
fileBP.AddContentLine("app();");
``` 
```cs
fileBP.Location = "c:/users/username/favorites";
```
```cs
fileBP.Location = Folder.Favorites.ToLocationString();
```

## 5. Creating new file
You mount the new file (or rewrite the existing one) through the Build method.
```cs
fileBP.Build();
```

## 6. Updating existing file
Just call the Build method whenever you want within your code.
- It will save the new changes into the existing file or create it anyways.
```cs
fileBP.ClearContent();
fileBP.Build();
```

## 7. Deleting existing file
You unmount the real file through the Unbuild method.
- It DOES NOT delete the blueprint object.
- The existence of the real file is checked inside Unbuild method... 
- ... so don't worry if you try to unbuild an inexisting file.
```cs
fileBP.Unbuild();
```

## 8. Cloning blueprint objects
GetCopy method is a quick way to create a new blueprint.
- Adds a copy number after the original name ("MyFileName_1").
```cs
FileBlueprint fileBPCopy = fileBP.GetCopy();
```
- But you can change the Name property later anyways, just like any other one.
```cs
fileBPCopy.Name = "NewName";
fileBPCopy.ClearContent();
fileBPCopy.AddContentLine("I am a copy!");
fileBPCopy.Build();
```
- If you delete the original blueprint, the copy will not be affected.
```cs
fileBP.Unbuild();
Console.Write(fileBPCopy.Content);
```
