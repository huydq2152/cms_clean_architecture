namespace Shared.SeedWork;

public class PagedResultMetaData
{
    public int PageIndex { get; set; }

    public int PageCount
    {
        get
        {
            var pageCount = (double)RowCount / PageSize;
            return (int)Math.Ceiling(pageCount);
        }
        set
        {
            if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
            PageCount = value;
        }
    }

    public int PageSize { get; set; }

    public int RowCount { get; set; }

    public bool HasPrevious => PageIndex > 1;

    public bool HasNext => PageIndex < PageCount;

    public int FirstRowOnPage => RowCount > 0 ? (PageIndex - 1) * PageSize + 1 : 0;

    public int LastRowOnPage => (int)Math.Min(PageIndex * PageSize, RowCount);
}