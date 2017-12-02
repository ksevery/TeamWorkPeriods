using System;
using System.Collections.Generic;
using TeamWorkhours.Engine.Models;

namespace TeamWorkhours.Engine
{
    public class TeamWorkPeriodEngine : ITeamWorkPeriodEngine
    {
        public (EmployeeTeamWorkPeriod FirstEmployee, EmployeeTeamWorkPeriod SecondEmployee) GetLongestPeriodEmployees(IEnumerable<EmployeeTeamWorkPeriod> employeesInTeams)
        {
            throw new NotImplementedException();
        }
    }
}
