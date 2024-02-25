using Shared.SeedWork;

namespace Shared
{
    public class PaginatedApiResult<T> : ApiResult<T>
    {
        public PaginatedApiResult(List<T> data)
        {
            Data = data;
        }

        public PaginatedApiResult(bool succeeded, List<T> data = default, string messages = null, int count = 0, int pageIndex = 1, int pageSize = 10)
        {
            Data = data;
            PageIndex = pageIndex;
            Succeeded = succeeded;
            Messages = messages;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }

        public new List<T> Data { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static PaginatedApiResult<T> Create(List<T> data, int count, int pageIndex, int pageSize)
        {
            return new PaginatedApiResult<T>(true, data, null, count, pageIndex, pageSize);
        }
    }
}
