namespace CleanArchitectureDemo.Application.Interfaces.Common
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
        DateTime Now { get; }
    }
}
