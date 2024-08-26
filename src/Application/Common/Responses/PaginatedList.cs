using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Platform.Application.Common.Responses;

/// <summary>
/// Represents a paginated list of items of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of items in the paginated list.</typeparam>
public record PaginatedList<T>
{
    /// <summary>
    /// Gets the current page number.
    /// </summary>
    public int CurrentPage { get; init; }

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int TotalPages { get; init; }

    /// <summary>
    /// Gets the total number of items.
    /// </summary>
    public int TotalItems { get; init; }

    /// <summary>
    /// Gets the list of items on the current page.
    /// </summary>
    public List<T> Result { get; init; } = new List<T>();

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class with the specified items and pagination details.
    /// </summary>
    /// <param name="items">The list of items on the current page.</param>
    /// <param name="count">The total number of items.</param>
    /// <param name="currentPage">The current page number.</param>
    /// <param name="pageSize">The number of items per page.</param>
    public PaginatedList(List<T> items, int count, int currentPage, int pageSize)
    {
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalItems = count;
        Result.AddRange(items);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
    /// </summary>
    public PaginatedList() { }
}

/// <summary>
/// Provides helper methods for creating <see cref="PaginatedList{T}"/> instances.
/// </summary>
public static class PaginatedListHelper
{
    /// <summary>
    /// The default number of items per page.
    /// </summary>
    public const int DefaultPageSize = 15;

    /// <summary>
    /// The default page number to start from.
    /// </summary>
    public const int DefaultCurrentPage = 1;

    /// <summary>
    /// Creates a paginated list from an <see cref="IQueryable{T}"/> source asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of items in the list.</typeparam>
    /// <param name="source">The source <see cref="IQueryable{T}"/> to paginate.</param>
    /// <param name="currentPage">The current page number.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the paginated list of items.</returns>
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(
        this IQueryable<T> source,
        int currentPage,
        int pageSize
    )
    {
        currentPage = currentPage > 0 ? currentPage : DefaultCurrentPage;
        pageSize = pageSize > 0 ? pageSize : DefaultPageSize;
        var count = await source.CountAsync();
        var items = await source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<T>(items, count, currentPage, pageSize);
    }
}
