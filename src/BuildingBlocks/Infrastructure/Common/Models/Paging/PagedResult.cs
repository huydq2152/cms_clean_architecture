using Microsoft.EntityFrameworkCore;
using Shared.SeedWork.Paging;

namespace Infrastructure.Common.Models.Paging;

public class PagedResult<T> : PagedResultMetaData where T : class
{
    public IEnumerable<T> Results { get; set; }

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