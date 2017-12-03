using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWorkPeriods.Engine.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public int TeamId { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public TimeSpan WorkedFor
        {
            get
            {
                return this.DateTo - this.DateFrom;
            }
        }
    }
}
