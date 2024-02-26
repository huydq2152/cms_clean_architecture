using Microsoft.EntityFrameworkCore;
using Shared.SeedWork;
using Shared.SeedWork.Paging;

namespace Infrastructure.Common.Models.Paging;

public class PagedResult<T> : PagedResultMetaData where T : class
{
    public List<T> Results { get; set; }

    public static async Task<PagedResult<T>> ToPagedList(IQueryable<T> source, int pageIndex, int pageSize)
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