using Microsoft.EntityFrameworkCore;
using Shared.SeedWork.Paging;

namespace Infrastructure.Common.Helpers.Paging;

/// <summary>
/// Represents a paginated result.
/// </summary>
/// <typeparam name="T">The type of the objects in the paginated result.</typeparam>
public class PagedResult<T> : PagedResultMetaData where T : class
{
    /// <summary>
    /// Gets or sets the results of the current page.
    /// </summary>
    public IEnumerable<T> Results { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="PagedResult{T}"/> class from the specified source.
    /// </summary>
    /// <param name="source">The source queryable.</param>
    /// <param name="pageIndex">The index of the current page.</param>
    /// <param name="pageSize">The size of the page.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="PagedResult{T}"/>.</returns>
    public static async Task<PagedResult<T>> ToPagedListAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize).ToListAsync();

        return new PagedResult<T>()
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            RowCount = count,
            Results = items
        };
    }
}