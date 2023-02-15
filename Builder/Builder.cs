namespace FileBuilder
{
    public static class Builder
    {
        /// <summary> Creates of updates this Container and its content (folders and files). </summary>
        public static void Build(this ContainerBlueprint container) => container.Build();

        /// <summary> Deletes this Container and its content (folders and files). </summary>
        public static void Unbuild(this ContainerBlueprint container) => container.Unbuild();
    }
}
