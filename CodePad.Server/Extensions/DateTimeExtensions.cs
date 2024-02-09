using System;
using System.Globalization;

public static class DateTimeExtensions
{
    public static DateTime ParseExact(this object obj, string format)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        if (string.IsNullOrEmpty(format))
        {
            throw new ArgumentException("Format cannot be null or empty.", nameof(format));
        }

        return DateTime.ParseExact(obj.ToString(), format, CultureInfo.CurrentCulture);
    }
}
