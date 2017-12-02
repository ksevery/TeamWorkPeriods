using System;
using System.Collections.Generic;
using System.Text;
using TeamWorkPeriods.Engine.Models;

namespace TeamWorkPeriods.Engine
{
    public interface ITeamWorkPeriodEngine
    {
        (EmployeeTeamWorkPeriod FirstEmployee, EmployeeTeamWorkPeriod SecondEmployee) GetLongestPeriodEmployees(IEnumerable<EmployeeTeamWorkPeriod> employeesInTeams);
    }
}
