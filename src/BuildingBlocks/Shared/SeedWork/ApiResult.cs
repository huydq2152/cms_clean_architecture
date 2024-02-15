using Shared.Interfaces;

namespace Shared.SeedWork
{
    public class ApiResult<T> : IApiResult<T>
    {
        public string Messages { get; set; } = string.Empty;

        public bool Succeeded { get; set; }

        public T Data { get; set; }

        public Exception Exception { get; set; }

        public int Code { get; set; }

        #region Non Async Methods 

        #region Success Methods 

        public static ApiResult<T> Success()
        {
            return new ApiResult<T> 
            { 
                Succeeded = true
            };
        }

        public static ApiResult<T> Success(string message)
        {
            return new ApiResult<T> 
            {
                Succeeded = true,
                Messages = message 
            };
        }

        public static ApiResult<T> Success(T data)
        {
            return new ApiResult<T>
            {
                Succeeded = true,
                Data = data
            };
        }

        public static ApiResult<T> Success(T data, string message)
        {
            return new ApiResult<T>
            {
                Succeeded = true,
                Messages = message,
                Data = data
            };
        }

        #endregion

        #region Failure Methods 

        public static ApiResult<T> Failure()
        {
            return new ApiResult<T>
            {
                Succeeded = false
            };
        }

        public static ApiResult<T> Failure(string messages)
        {
            return new ApiResult<T>
            {
                Succeeded = false,
                Messages = messages
            };
        }

        public static ApiResult<T> Failure(T data)
        {
            return new ApiResult<T>
            {
                Succeeded = false,
                Data = data
            };
        }

        public static ApiResult<T> Failure(T data, string messages)
        {
            return new ApiResult<T>
            {
                Succeeded = false,
                Messages = messages,
                Data = data
            };
        }

        public static ApiResult<T> Failure(Exception exception)
        {
            return new ApiResult<T>
            {
                Succeeded = false,
                Exception = exception 
            };
        }

        #endregion

        #endregion

        #region Async Methods 

        #region Success Methods 

        public static Task<ApiResult<T>> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public static Task<ApiResult<T>> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }

        public static Task<ApiResult<T>> SuccessAsync(T data)
        {
            return Task.FromResult(Success(data));
        }

        public static Task<ApiResult<T>> SuccessAsync(T data, string message)
        {
            return Task.FromResult(Success(data, message));
        }

        #endregion

        #region Failure Methods 

        public static Task<ApiResult<T>> FailureAsync()
        {
            return Task.FromResult(Failure());
        }

        public static Task<ApiResult<T>> FailureAsync(string message)
        {
            return Task.FromResult(Failure(message));
        }

        public static Task<ApiResult<T>> FailureAsync(T data)
        {
            return Task.FromResult(Failure(data));
        }

        public static Task<ApiResult<T>> FailureAsync(T data, string message)
        {
            return Task.FromResult(Failure(data, message));
        }

        public static Task<ApiResult<T>> FailureAsync(Exception exception)
        {
            return Task.FromResult(Failure(exception));
        }

        #endregion

        #endregion
    }
}
