using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Linq;
using TeamWorkPeriods.Engine.Models;

namespace TeamWorkPeriods.Engine
{
    public class TeamWorkPeriodsEngine : ITeamWorkPeriodsEngine
    {
        /// <summary>
        /// Will return the two employees who've worked together the longest in the same team
        /// </summary>
        /// <param name="employees"></param>
        /// <returns>A <see cref="EmployeesDuo"/> object, or null if no two overlapping employees are found.</returns>
        public EmployeesDuo GetLongestPeriodEmployees(IEnumerable<Employee> employees)
        {
            // Will group employees by teams and get only the teams with more than 1 employee in them
            var employeeTeams = employees.GroupBy(e => e.TeamId).Where(t => t.Count() > 1);

            var topEmployeesPerTeam = new List<EmployeesDuo>();
            foreach (var team in employeeTeams)
            {
                var topEmployees = GetTopEmployees(team);

                topEmployeesPerTeam.Add(topEmployees);
            }

            var topDuo = topEmployeesPerTeam.OrderByDescending(e => e.WorkedTogetherFor).FirstOrDefault();

            return topDuo;
        }

        private EmployeesDuo GetTopEmployees(IGrouping<int, Employee> team)
        {
            EmployeesDuo topDuo = null;

            foreach (var employee in team)
            {
                var employeeRange = new TimeRange(employee.DateFrom, employee.DateTo);

                // Gets the top employee overlapping with the current one, excluding the current one
                var topOverlappingEmployee = team
                    .Select(e => new
                    {
                        Employee = e,
                        TimePeriod = new TimeRange(e.DateFrom, e.DateTo)
                    })
                    // Gets only employees that have worked with the current one, but are not the current one(obviously he/she has worked with him/herself)
                    .Where(e => employeeRange.IntersectsWith(e.TimePeriod) && e.Employee.EmployeeId != employee.EmployeeId)
                    // Orders by descending based on the overlapping time period for which the employees have worked together with the current one, top one is the one he's worked the longest with in this team
                    .OrderByDescending(e => employeeRange.GetIntersection(e.TimePeriod).Duration)
                    .FirstOrDefault();

                // Possible to have a case where only one employee is in the team - will not get process further
                if (topOverlappingEmployee != null)
                {
                    var intersection = employeeRange.GetIntersection(topOverlappingEmployee.TimePeriod);

                    if (topDuo == null)
                    {
                        topDuo = new EmployeesDuo()
                        {
                            FirstEmployee = employee,
                            SecondEmployee = topOverlappingEmployee.Employee,
                            WorkedTogetherFor = intersection.Duration
                        };
                    }
                    else
                    {
                        // Simply compares the time the current found top duo has worked for with the top one overall and overwrites if the current duo has worked together longer
                        if (topDuo.WorkedTogetherFor < intersection.Duration)
                        {
                            topDuo = new EmployeesDuo()
                            {
                                FirstEmployee = employee,
                                SecondEmployee = topOverlappingEmployee.Employee,
                                WorkedTogetherFor = intersection.Duration
                            };
                        }
                    }
                }
            }

            return topDuo;
        }
    }
}
