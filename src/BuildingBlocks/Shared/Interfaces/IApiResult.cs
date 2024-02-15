namespace Shared.Interfaces
{
    public interface IApiResult<T>
    {
        string Messages { get; set; }

        bool Succeeded { get; set; }

        T Data { get; set; }

        Exception Exception { get; set; }

        int Code { get; set; }
    }
}
