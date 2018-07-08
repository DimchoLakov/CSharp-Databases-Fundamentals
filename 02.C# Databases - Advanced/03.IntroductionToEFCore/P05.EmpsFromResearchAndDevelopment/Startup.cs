using System;
using P02_DatabaseFirst.Data;
using System.Linq;

namespace P05.EmpsFromResearchAndDevelopment
{
    public class Startup
    {
        public static void Main()
        {
            var dbContext = new SoftUniContext();

            using (dbContext)
            {
                //05.
                var selectedEmployees = dbContext
                    .Employees
                    .Where(e => e.Department.Name == "Research and Development")
                    .OrderBy(e => e.Salary)
                    .ThenByDescending(e => e.FirstName)
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        DepartmentName = e.Department.Name,
                        e.Salary
                    });

                foreach (var se in selectedEmployees)
                {
                    Console.WriteLine($"{se.FirstName} {se.LastName} from {se.DepartmentName} - ${se.Salary:f2}");
                }
            }
        }
    }
}
