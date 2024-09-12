namespace Infrastructure.Extensions.Collection;

/// <summary>
/// Extension methods for <see cref="T:System.Collections.Generic.IList`1" />.
/// </summary>
public static class ListExtensions
{
    /// <summary>
    /// Sort a list by a topological sorting, which consider their  dependencies
    /// </summary>
    /// <typeparam name="T">The type of the members of values.</typeparam>
    /// <param name="source">A list of objects to sort</param>
    /// <param name="getDependencies">Function to resolve the dependencies</param>
    /// <returns></returns>
    public static List<T> SortByDependencies<T>(
        this IEnumerable<T> source,
        Func<T, IEnumerable<T>> getDependencies)
    {
        List<T> sorted = new List<T>();
        Dictionary<T, bool> visited = new Dictionary<T, bool>();
        foreach (T obj in source)
            ListExtensions.SortByDependenciesVisit<T>(obj, getDependencies, sorted, visited);
        return sorted;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">The type of the members of values.</typeparam>
    /// <param name="item">Item to resolve</param>
    /// <param name="getDependencies">Function to resolve the dependencies</param>
    /// <param name="sorted">List with the sortet items</param>
    /// <param name="visited">Dictionary with the visited items</param>
    private static void SortByDependenciesVisit<T>(
        T item,
        Func<T, IEnumerable<T>> getDependencies,
        List<T> sorted,
        Dictionary<T, bool> visited)
    {
        bool flag;
        if (visited.TryGetValue(item, out flag))
        {
            if (flag)
                throw new ArgumentException("Cyclic dependency found! Item: " + item?.ToString());
        }
        else
        {
            visited[item] = true;
            IEnumerable<T> objs = getDependencies(item);
            if (objs != null)
            {
                foreach (T obj in objs)
                    ListExtensions.SortByDependenciesVisit<T>(obj, getDependencies, sorted, visited);
            }

            visited[item] = false;
            sorted.Add(item);
        }
    }
}