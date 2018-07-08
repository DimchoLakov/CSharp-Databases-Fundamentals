using System;
using System.Linq;
using P02_DatabaseFirst.Data;

namespace P09.Employee147
{
    public class Startup
    {
        public static void Main()
        {
            using (var dbContext = new SoftUniContext())
            {
                var emp = dbContext
                    .Employees
                    .Where(e => e.EmployeeId == 147)
                    .Select(e => new
                    {
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        JobTitle = e.JobTitle,
                        Projects = e.EmployeesProjects.Select(ep => new
                        {
                            EmpProjName = ep.Project.Name
                        })
                    })
                    .FirstOrDefault();

                Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle}");

                emp
                    .Projects
                    .OrderBy(p => p.EmpProjName)
                    .ToList()
                    .ForEach(p => Console.WriteLine(p.EmpProjName));
            }
        }
    }
}
