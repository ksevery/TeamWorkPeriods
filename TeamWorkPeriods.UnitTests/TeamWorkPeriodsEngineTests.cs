using System;
using System.Collections.Generic;
using TeamWorkPeriods.Engine;
using TeamWorkPeriods.Engine.Models;
using Xunit;

namespace TeamWorkPeriods.UnitTests
{
    public class TeamWorkPeriodsEngineTests
    {
        [Fact]
        public void OnePairOfOverlappingEmployees_ShouldBeReturned()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { EmployeeId = 1, TeamId = 1, DateFrom = new DateTime(2017, 5, 5), DateTo = new DateTime(2017, 5, 10) },
                new Employee { EmployeeId = 2, TeamId = 1, DateFrom = new DateTime(2017, 5, 5), DateTo = new DateTime(2017, 5, 10) }
            };

            var engine = new TeamWorkPeriodsEngine();

            // Act
            var result = engine.GetLongestPeriodEmployees(employees);

            // Assert
            Assert.Equal(TimeSpan.FromDays(5), result.WorkedTogetherFor);
        }

        [Fact]
        public void TwoPairsOfOverlappingEmployees_ShouldReturnLongerOverlappingEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { EmployeeId = 1, TeamId = 1, DateFrom = new DateTime(2017, 5, 5), DateTo = new DateTime(2017, 5, 10) },
                new Employee { EmployeeId = 2, TeamId = 1, DateFrom = new DateTime(2017, 5, 5), DateTo = new DateTime(2017, 5, 10) },
                new Employee { EmployeeId = 3, TeamId = 2, DateFrom = new DateTime(2017, 5, 5), DateTo = new DateTime(2017, 5, 8) },
                new Employee { EmployeeId = 4, TeamId = 2, DateFrom = new DateTime(2017, 5, 5), DateTo = new DateTime(2017, 5, 10) }
            };

            var engine = new TeamWorkPeriodsEngine();

            // Act
            var result = engine.GetLongestPeriodEmployees(employees);

            // Assert
            Assert.Equal(TimeSpan.FromDays(5), result.WorkedTogetherFor);
            Assert.Equal(1, result.FirstEmployee.TeamId);
            Assert.InRange(result.FirstEmployee.EmployeeId, 1, 2);
            Assert.InRange(result.SecondEmployee.EmployeeId, 1, 2);
        }
    }
}
