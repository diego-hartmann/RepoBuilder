
namespace FileBuilder
{
    public static class Factory
    {
        public static void Build(FolderBlueprint folderBlueprint) => folderBlueprint.Build();
        public static void Unbuild(FolderBlueprint folderBlueprint) => folderBlueprint.Unbuild();
    }
}
