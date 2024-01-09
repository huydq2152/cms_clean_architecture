using CleanArchitectureDemo.Application.Interfaces;
using CleanArchitectureDemo.Application.Interfaces.Common;

namespace CleanArchitectureDemo.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
        public DateTime Now => DateTime.Now;
    }
}