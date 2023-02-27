# Introduction
[Youtube tutorial](https://www.youtube.com/playlist?list=PLZFUL050KiAmBiiz4UsIXghblFbOaja2L)
[See slides](https://drive.google.com/drive/folders/1qsDnCVjjpoVFMoGbE0Qk_zWHBG59V_ed?usp=share_link)
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

## 1. RootBlueprint
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

## 2. FolderBlueprint
- Creates the blueprint of any sub directory using the FolderBlueprint constructor. 
```cs
var folder = new FolderBlueprint("Folder");
```
- You might want to add it into the root directory.
```cs
root.Add(folder);
//or
folder.MoveTo(root);
```

## 3. DocumentBlueprint
- Creates the blueprint of any document using the DocumentBlueprint constructor. 
```cs
// creating python file
var PY = new DocumentBlueprint("Doc_PY", Extention.Python);
// creating javascript file
var JS = new DocumentBlueprint("Doc_JS", Extention.JavaScript);
```
- Add text content inside any document. 
```cs
JS.WriteLine("import React from 'react';");
JS.WriteLine("const App = props => <div> {props.title} </div>;");
JS.WriteLine("export { App };");
JS.Write(@"
    function helper(parameter){
        console.log(parameter.toString());
    }
    export { helper };
");
```
- You might want to add them into the any folder you see fit.
```cs
// adding python file into root
root.Add(PY);
// adding javascript file into root subfolder
folder.Add(JS);
```
