using System.Linq.Expressions;

namespace Platform.Application.Extensions;

/// <summary>
/// Provides extension methods for filtering data.
/// </summary>
public static class FilterExtension
{
    /// <summary>
    /// Filters a sequence of values based on a condition if the condition is true.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of the source sequence.</typeparam>
    /// <param name="source">The source sequence to filter.</param>
    /// <param name="condition">The condition to check.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns>The filtered sequence if the condition is true; otherwise, the original sequence.</returns>
    public static IQueryable<TSource> WhereIf<TSource>(
        this IQueryable<TSource> source,
        bool condition,
        Expression<Func<TSource, bool>> predicate
    )
    {
        if (condition)
            return source.Where(predicate);

        return source;
    }
}
