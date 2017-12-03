using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TeamWorkPeriods.Engine.Converters;
using Xunit;

namespace TeamWorkPeriods.UnitTests
{
    public class TeamWorkPeriodsDataConverterTests
    {
        [Fact]
        public void ValidStream_ShouldConvertToListOfEmployees()
        {
            // Arrange
            var data = $"1, 1, 2010-11-01, 2017-11-06{Environment.NewLine}2, 1, 2010-11-01, 2017-11-06";

            var stringStream = new MemoryStream(Encoding.UTF8.GetBytes(data));

            // Act
            var result = DataConverter.Convert(stringStream);

            // Assert
            var firstEmployee = result.First();
            var secondEmployee = result.OrderBy(r => r.EmployeeId).Skip(1).First();

            Assert.NotEmpty(result);
            Assert.Equal(1, firstEmployee.EmployeeId);
            Assert.Equal(1, firstEmployee.TeamId);
            Assert.Equal(new DateTime(2010, 11, 1).Date, firstEmployee.DateFrom.Date);
            Assert.Equal(new DateTime(2017, 11, 6).Date, firstEmployee.DateTo.Date);
            Assert.Equal(2, secondEmployee.EmployeeId);
            Assert.Equal(1, secondEmployee.TeamId);
        }
    }
}
