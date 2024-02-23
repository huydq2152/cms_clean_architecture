namespace Shared.SeedWork;

public class PagedResultMetaData
{
    public int CurrentPage { get; set; }

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

    public bool HasPrevious => CurrentPage > 1;

    public bool HasNext => CurrentPage < PageCount;

    public int FirstRowOnPage => RowCount > 0 ? (CurrentPage - 1) * PageSize + 1 : 0;

    public int LastRowOnPage => (int)Math.Min(CurrentPage * PageSize, RowCount);
}