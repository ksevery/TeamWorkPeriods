namespace TeamWorkPeriods.Client
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using TeamWorkPeriods.Engine;
    using TeamWorkPeriods.Engine.Converters;
    using TeamWorkPeriods.Engine.Models;

    public class Program
    {
        private const string TerminationLiteral = "EXIT";

        public static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.Write("Please insert a path to the text file for Employees and Teams, or type EXIT to end the application: ");
                    var input = Console.ReadLine();

                    if (input.ToUpper() == TerminationLiteral)
                    {
                        break;
                    }

                    if (!File.Exists(input))
                    {
                        Console.WriteLine("File does not exist - please input a valid path to a text file!");
                        continue;
                    }

                    var fileStream = new FileStream(input, FileMode.Open);

                    var employeesTeamsPeriods = DataConverter.Convert(fileStream);

                    if (employeesTeamsPeriods == null)
                    {
                        Console.WriteLine("File is empty, or is not in the proper format!(HINT: EmpID, ProjectID, DateFrom, DateTo)");
                        continue;
                    }

                    var engine = new TeamWorkPeriodEngine();
                    var employees = engine.GetLongestPeriodEmployees(employeesTeamsPeriods);

                    Console.WriteLine($"The employees who've worked together the longest are: {employees.FirstEmployee.EmployeeId} and {employees.SecondEmployee.EmployeeId}");
                    Console.Write("If you'd like to continue, enter any key, or type in EXIT to exit the application: ");
                    var endOfCycleCommand = Console.ReadLine();

                    if (endOfCycleCommand == TerminationLiteral)
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                    
                }
                
            }
            
        }
    }
}
