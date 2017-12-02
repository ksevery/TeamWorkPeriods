using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TeamWorkPeriods.Engine
{
    public static class Extensions
    {
        public static DateTime? TryParseExactNullable(this DateTime dateTime, string input, string format)
        {
            DateTime date;
            if (DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out date))
            {
                return date;
            }
            else
            {
                return null;
            }
        }
    }
}
