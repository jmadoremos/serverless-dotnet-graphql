namespace GraphQL.Extensions;

public static class IEnumerableExtension
{
    public static IEnumerable<T> Apply<T>(
        this IEnumerable<T> @this,
        Action<T> action)
    {
        foreach (var e in @this)
        {
            action(e);
            yield return e;
        }
    }

    public static bool IsAnyNull<T>(this IEnumerable<T> @this)
    {
        foreach (var e in @this)
        {
            if (e is null)
            {
                return true;
            }
        }

        return false;
    }
}
