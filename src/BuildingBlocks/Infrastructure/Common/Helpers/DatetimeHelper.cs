using Infrastructure.Extensions;

namespace Infrastructure.Common.Helpers;

public static class DatetimeHelper
{
    public static class DateTimeHelper
    {
        #region QuarterRange

        public static (DateTime startDate, DateTime endDate) GetQuarterRange(int quarter, int year)
        {
            var targetDate = new DateTime(year, quarter * 3, 1);
            var result = GetQuarterRange(targetDate);
            return (result.startDate, result.endDate);
        }

        private static (DateTime startDate, DateTime endDate) GetQuarterRange(DateTime input)
        {
            var inputMonth = input.Month;
            var inputYear = input.Year;

            var startDate = new DateTime();
            var endDate = new DateTime();

            if (inputMonth.IsBetween(1, 3))
            {
                startDate = new DateTime(inputYear, 1, 1);
                endDate = startDate.AddMonths(3).AddMinutes(-1);
            }

            if (inputMonth.IsBetween(4, 6))
            {
                startDate = new DateTime(inputYear, 4, 1);
                endDate = startDate.AddMonths(3).AddMinutes(-1);
            }

            if (inputMonth.IsBetween(7, 9))
            {
                startDate = new DateTime(inputYear, 7, 1);
                endDate = startDate.AddMonths(3).AddMinutes(-1);
            }

            if (inputMonth.IsBetween(10, 12))
            {
                startDate = new DateTime(inputYear, 10, 1);
                endDate = startDate.AddMonths(3).AddMinutes(-1);
            }

            return (startDate, endDate);
        }

        #endregion
    }
}