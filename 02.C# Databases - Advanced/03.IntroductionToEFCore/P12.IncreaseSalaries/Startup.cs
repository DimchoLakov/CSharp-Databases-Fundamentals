using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using P02_DatabaseFirst.Data;

namespace P12.IncreaseSalaries
{
    public class Startup
    {
        public static void Main()
        {
            using (var dbContext = new SoftUniContext())
            {
                string[] departments = new string[]
                {
                    "Engineering",
                    "Tool Design",
                    "Marketing",
                    "Information Services"
                };

                var employees = dbContext
                    .Employees
                    .Where(e => departments.Any(d => d == e.Department.Name))
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName);

                foreach (var employee in employees)
                {
                    employee.Salary += employee.Salary * (decimal)0.12;
                }
                
                using (StreamWriter sw = new StreamWriter("../../../Employees.txt"))
                {
                    foreach (var employee in employees)
                    {
                        Console.WriteLine(employee.FirstName + " " + employee.LastName + $" (${employee.Salary:F2})");

                        sw.WriteLine(employee.FirstName + " " + employee.LastName + $" (${employee.Salary:F2})");
                    }
                }

                dbContext.SaveChanges();
            }
        }
    }
}
