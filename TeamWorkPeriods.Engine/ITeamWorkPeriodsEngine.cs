using System;
using System.Collections.Generic;
using System.Text;
using TeamWorkPeriods.Engine.Models;

namespace TeamWorkPeriods.Engine
{
    public interface ITeamWorkPeriodsEngine
    {
        EmployeesDuo GetLongestPeriodEmployees(IEnumerable<Employee> employees);
    }
}
