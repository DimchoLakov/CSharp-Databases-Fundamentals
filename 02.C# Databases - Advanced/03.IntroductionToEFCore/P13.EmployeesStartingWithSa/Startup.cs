using System;
using System.IO;
using System.Linq;
using P02_DatabaseFirst.Data;

namespace P13.EmployeesStartingWithSa
{
    public class Startup
    {
        public static void Main()
        {
            using (var dbContext = new SoftUniContext())
            {
                var employees = dbContext
                    .Employees
                    .Where(e => e.FirstName.StartsWith("Sa"))
                    .Select(e => new
                    {
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        JobTitle = e.JobTitle,
                        Salary = e.Salary
                    })
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName);

                using (StreamWriter sw = new StreamWriter("../../../Employees.txt"))
                {
                    foreach (var e in employees)
                    {
                        Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})");
                        sw.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})");
                    }
                }
            }
        }
    }
}
