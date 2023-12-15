namespace Chcklst.Shared.Extensions;

public static class EnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> doAction)
    {
        foreach (var (item, index) in enumerable.Select((item, index) => (item, index)))
        {
            doAction(item, index);
        }
    }
}