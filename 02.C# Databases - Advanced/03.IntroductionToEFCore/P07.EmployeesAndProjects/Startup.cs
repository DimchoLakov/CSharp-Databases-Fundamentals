using System;
using P02_DatabaseFirst.Data;
using System.Linq;

namespace P07.EmployeesAndProjects
{
    public class Startup
    {
        public static void Main()
        {
            var dbContext = new SoftUniContext();

            using (dbContext)
            {
                var employeesProjects = dbContext
                    .Employees
                    .Where(e => e.EmployeesProjects
                        .Any(ep => ep.Project.StartDate.Year >= 2001 && ep.Project.EndDate.Value.Year <= 2003))
                    .Take(30)
                    .Select(e => new
                    {
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        ManagerFirstName = e.Manager.FirstName,
                        ManagerLastName = e.Manager.LastName,
                        Projects = e.EmployeesProjects.Select(ep => new
                        {
                            ProjectName = ep.Project.Name,
                            StartDate = ep.Project.StartDate,
                            EndDate = ep.Project.EndDate
                        })
                    });

                foreach (var ep in employeesProjects)
                {
                    Console.WriteLine($"{ep.FirstName} {ep.LastName} - Manager: {ep.ManagerFirstName} {ep.ManagerLastName}");

                    foreach (var project in ep.Projects)
                    {
                        string startDate = project.StartDate.ToString("M/d/yyyy h:mm:ss tt");

                        string endDate =
                            project.EndDate == null ? "not finished" : ((DateTime)project.EndDate).ToString("M/d/yyyy h:mm:ss tt");

                        Console.WriteLine($"--{project.ProjectName} - {startDate} - {endDate}");
                    }
                }
            }
        }
    }
}
