namespace Contracts.Common.Models.Api
{
    public class ApiErrorResult 
    {
        public string Messages { get; set; } = string.Empty;

        public Exception Exception { get; set; }
    }
}
