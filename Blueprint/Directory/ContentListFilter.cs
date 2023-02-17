using System.Collections.Generic;

namespace RepoBuilder
{
    internal static class ContentListFilter
    {
        internal static List<T> Filter<T>(this List<Blueprint> content) where T : Blueprint
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
