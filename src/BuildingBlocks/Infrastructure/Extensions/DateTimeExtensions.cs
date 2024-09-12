namespace Infrastructure.Extensions;

/// <summary>
/// Extension methods for <see cref="T:System.DateTime" />.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>Converts a DateTime to a Unix Timestamp</summary>
    /// <param name="target">This DateTime</param>
    /// <returns></returns>
    public static double ToUnixTimestamp(this DateTime target)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Math.Floor((target - dateTime).TotalSeconds);
    }

    /// <summary>Converts a Unix Timestamp in to a DateTime</summary>
    /// <param name="unixTime">This Unix Timestamp</param>
    /// <returns></returns>
    public static DateTime FromUnixTimestamp(this double unixTime) =>
        new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(unixTime);

    /// <summary>Gets the value of the End of the day (23:59)</summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public static DateTime ToDayEnd(this DateTime target)
    {
        DateTime dateTime = target.Date;
        dateTime = dateTime.AddDays(1.0);
        return dateTime.AddMilliseconds(-1.0);
    }

    /// <summary>
    /// Gets the First Date of the week for the specified date
    /// </summary>
    /// <param name="dt">this DateTime</param>
    /// <param name="startOfWeek">The Start Day of the Week (ie, Sunday/Monday)</param>
    /// <returns>The First Date of the week</returns>
    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
    {
        int num = dt.DayOfWeek - startOfWeek;
        if (num < 0)
            num += 7;
        return dt.AddDays((double)(-1 * num)).Date;
    }

    /// <summary>Returns all the days of a month.</summary>
    /// <param name="year">The year.</param>
    /// <param name="month">The month.</param>
    /// <returns></returns>
    public static IEnumerable<DateTime> DaysOfMonth(int year, int month) => Enumerable
        .Range(0, DateTime.DaysInMonth(year, month))
        .Select<int, DateTime>((Func<int, DateTime>)(day => new DateTime(year, month, day + 1)));

    /// <summary>
    /// Determines the Nth instance of a Date's DayOfWeek in a month
    /// </summary>
    /// <returns></returns>
    /// <example>11/29/2011 would return 5, because it is the 5th Tuesday of each month</example>
    public static int WeekDayInstanceOfMonth(this DateTime dateTime)
    {
        int y = 0;
        return DateTimeExtensions.DaysOfMonth(dateTime.Year, dateTime.Month)
            .Where<DateTime>((Func<DateTime, bool>)(date => dateTime.DayOfWeek.Equals((object)date.DayOfWeek))).Select(
                x => new
                {
                    n = ++y,
                    date = x
                }).Where(x => x.date.Equals(new DateTime(dateTime.Year, dateTime.Month, dateTime.Day))).Select(x => x.n)
            .FirstOrDefault<int>();
    }

    /// <summary>Gets the total days in a month</summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    public static int TotalDaysInMonth(this DateTime dateTime) =>
        DateTimeExtensions.DaysOfMonth(dateTime.Year, dateTime.Month).Count<DateTime>();

    /// <summary>
    /// Takes any date and returns it's value as an Unspecified DateTime
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static DateTime ToDateTimeUnspecified(this DateTime date) => date.Kind == DateTimeKind.Unspecified
        ? date
        : new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, DateTimeKind.Unspecified);

    /// <summary>Trims the milliseconds off of a datetime</summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static DateTime TrimMilliseconds(this DateTime date) => new DateTime(date.Year, date.Month, date.Day,
        date.Hour, date.Minute, date.Second, date.Kind);
}