namespace FileBuilder
{
    public static class Builder
    {
        public static void Build(this ContainerBlueprint container) => container.Build();
        public static void Unbuild(this ContainerBlueprint container) => container.Unbuild();
    }
}
