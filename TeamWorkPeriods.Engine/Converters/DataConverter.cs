using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using TeamWorkPeriods.Engine.Models;

namespace TeamWorkPeriods.Engine.Converters
{
    public static class DataConverter
    {
        /// <summary>
        /// Reads the given stream line-by-line and converts every line to the Employee object.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>A list of <see cref="Employee" /> objects</returns>
        /// <exception cref="ConvertException">Will be thrown if any error occurs during the conversion process, contains the text that failed conversion</exception>
        public static IEnumerable<Employee> Convert(Stream stream)
        {
            var result = new List<Employee>();

            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var parts = line.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        var lineModel = new Employee
                        {
                            EmployeeId = int.Parse(parts[0]),
                            TeamId = int.Parse(parts[1]),
                            DateFrom = DateTime.ParseExact(parts[2], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                            DateTo = ParseDateWithNull(parts[3], "yyyy-MM-dd")
                        };

                        result.Add(lineModel);
                    }
                    catch (Exception ex)
                    {
                        throw new ConvertException($"Error during data conversion on line:  {line}", ex);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Will try to parse the date to a the given format, if it fails - it will return the current DateTime
        /// </summary>
        /// <param name="input"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        private static DateTime ParseDateWithNull(string input, string format)
        {
            DateTime date;
            if (DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out date))
            {
                return date;
            }
            else
            {
                return DateTime.Now;
            }
        }
    }
}
