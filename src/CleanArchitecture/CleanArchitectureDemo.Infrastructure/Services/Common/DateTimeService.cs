using CleanArchitectureDemo.Application.Interfaces.Services.Common;

namespace CleanArchitectureDemo.Infrastructure.Services.Common
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
        public DateTime Now => DateTime.Now;
    }
}