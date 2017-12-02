using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using TeamWorkhours.Engine.Models;

namespace TeamWorkhours.Engine.Converters
{
    public static class DataConverter
    {
        public static IEnumerable<EmployeeTeamWorkPeriod> Convert(FileStream stream)
        {
            var result = new List<EmployeeTeamWorkPeriod>();

            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var parts = line.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        var lineModel = new EmployeeTeamWorkPeriod
                        {
                            EmployeeId = int.Parse(parts[0]),
                            TeamId = int.Parse(parts[1]),
                            DateFrom = DateTime.ParseExact(parts[2], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                            DateTo = TryParseExactNullable(parts[3], "yyyy-MM-dd")
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

        public static DateTime? TryParseExactNullable(string input, string format)
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
