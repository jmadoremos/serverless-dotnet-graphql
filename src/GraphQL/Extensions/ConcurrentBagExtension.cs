namespace GraphQL.Extensions;

using System.Collections.Concurrent;

public static class ConcurrentBagExtension
{
    public static void AddRange<T>(
        this ConcurrentBag<T> @this,
        IEnumerable<T> toAdd)
    {
        foreach (var element in toAdd)
        {
            @this.Add(element);
        }
    }
}
