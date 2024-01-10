namespace CleanArchitectureDemo.Application.Interfaces.Services.Common
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
        DateTime Now { get; }
    }
}
