using Shared.SeedWork;

namespace Shared
{
    public class PaginatedApiResult<T> : ApiResult<T>
    {
        public PaginatedApiResult(List<T> data)
        {
            Data = data;
        }

        public PaginatedApiResult(bool succeeded, List<T> data = default, string messages = null, int count = 0, int pageNumber = 1, int pageSize = 10)
        {
            Data = data;
            CurrentPage = pageNumber;
            Succeeded = succeeded;
            Messages = messages;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }

        public new List<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public static PaginatedApiResult<T> Create(List<T> data, int count, int pageNumber, int pageSize)
        {
            return new PaginatedApiResult<T>(true, data, null, count, pageNumber, pageSize);
        }
    }
}
