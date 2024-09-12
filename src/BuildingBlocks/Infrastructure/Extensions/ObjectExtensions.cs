using System.ComponentModel;
using System.Globalization;

namespace Infrastructure.Extensions;

/// <summary>Extension methods for all objects.</summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Used to simplify and beautify casting an object to a type.
    /// </summary>
    /// <typeparam name="T">Type to be casted</typeparam>
    /// <param name="obj">Object to cast</param>
    /// <returns>Casted object</returns>
    public static T As<T>(this object obj) where T : class => (T)obj;

    /// <summary>
    /// Converts given object to a value or enum type using <see cref="M:System.Convert.ChangeType(System.Object,System.TypeCode)" /> or <see cref="M:System.Enum.Parse(System.Type,System.String)" /> method.
    /// </summary>
    /// <param name="obj">Object to be converted</param>
    /// <typeparam name="T">Type of the target object</typeparam>
    /// <returns>Converted object</returns>
    public static T To<T>(this object obj) where T : struct
    {
        if (typeof(T) == typeof(Guid) || typeof(T) == typeof(TimeSpan))
            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(obj.ToString());
        if (!typeof(T).IsEnum)
            return (T)Convert.ChangeType(obj, typeof(T), (IFormatProvider)CultureInfo.InvariantCulture);
        return Enum.IsDefined(typeof(T), obj)
            ? (T)Enum.Parse(typeof(T), obj.ToString())
            : throw new ArgumentException(string.Format("Enum type undefined '{0}'.", obj));
    }

    /// <summary>Check if an item is in a list.</summary>
    /// <param name="item">Item to check</param>
    /// <param name="list">List of items</param>
    /// <typeparam name="T">Type of the items</typeparam>
    public static bool IsIn<T>(this T item, params T[] list) => ((IEnumerable<T>)list).Contains<T>(item);
}