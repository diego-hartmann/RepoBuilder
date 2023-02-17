using System.Collections.Generic;

namespace RepoBuilder
{
    internal static class Filter
    {
        internal static List<T> OnlyOfType<T>(this List<Blueprint> content) where T : Blueprint
        {
            var newList = new List<T>();
            
            content.ForEach( item =>
            {
                if (item is T) newList.Add(item as T);
            });

            return newList;
        }
    }
}
