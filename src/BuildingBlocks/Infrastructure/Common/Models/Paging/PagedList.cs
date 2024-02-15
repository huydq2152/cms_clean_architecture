using Microsoft.EntityFrameworkCore;
using MetaData = Shared.SeedWork.MetaData;

namespace Infrastructure.Common.Models.Paging;

public class PagedList<T>: List<T>
{
    public PagedList(IEnumerable<T> items, int totalItems, int currentPage, int pageSize)
    {
        _metaData = new MetaData()
        {
            TotalCount = totalItems,
            PageSize = pageSize,
            PageNumber = currentPage,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
        };
        AddRange(items);
    }

    private MetaData _metaData { get; }

    public MetaData GetMetaData()
    {
        return _metaData;
    }
    
    public static async Task<PagedList<T>> ToPagedList(IQueryable<T> source, int currentPage, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize).ToListAsync();

        return new PagedList<T>(items, count, currentPage, pageSize);
    }
}