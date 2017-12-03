using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWorkPeriods.Engine.Models
{
    // Contains only two employees
    public class EmployeesDuo
    {
        public Employee FirstEmployee { get; set; }

        public Employee SecondEmployee { get; set; }

        public TimeSpan WorkedTogetherFor { get; set; }
    }
}
