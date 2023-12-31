namespace Ardalis.GuardClauses;

public static class DoesntContainGuard
{
    public static void Contains<T>(this IGuardClause guardClause, T actualValue, string paramName, IEnumerable<T> enumerable)
    {
        if (enumerable.Contains(actualValue))
        {
            throw new ArgumentException($"The collection contains the value for {paramName}: {actualValue}.", paramName);
        }
    }

    public static void DoesntContain<T>(this IGuardClause guardClause, T actualValue, string paramName, IEnumerable<T> enumerable)
    {
        if (!enumerable.Contains(actualValue))
        {
            throw new ArgumentException($"The collection doesn't contain the value for {paramName}: {actualValue}.", paramName);
        }
    }
}
