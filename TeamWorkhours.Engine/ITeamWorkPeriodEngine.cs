using System;
using System.Collections.Generic;
using System.Text;
using TeamWorkhours.Engine.Models;

namespace TeamWorkhours.Engine
{
    public interface ITeamWorkPeriodEngine
    {
        (EmployeeTeamWorkPeriod FirstEmployee, EmployeeTeamWorkPeriod SecondEmployee) GetLongestPeriodEmployees(IEnumerable<EmployeeTeamWorkPeriod> employeesInTeams);
    }
}
