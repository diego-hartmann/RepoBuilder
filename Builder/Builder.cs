namespace RepoBuilder
{
    public static class Builder
    {
        /// <summary> Creates of updates this Root and its content (folders and files). </summary>
        public static void Build(this RootBlueprint Root) => Root.Build();

        /// <summary> Deletes this Root and its content (folders and files). </summary>
        public static void Unbuild(this RootBlueprint Root) => Root.Unbuild();
    }
}
