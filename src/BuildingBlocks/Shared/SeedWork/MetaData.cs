namespace Shared.SeedWork;

public class MetaData
{
    public int PageNumber { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public bool HasPrevious => PageNumber > 1;

    public bool HasNext => PageNumber < TotalPages;

    public int FirstRowOnPage => TotalCount > 0 ? (PageNumber - 1) * PageSize + 1 : 0;

    public int LastRowOnPage => (int)Math.Min(PageNumber * PageSize, TotalCount);
}