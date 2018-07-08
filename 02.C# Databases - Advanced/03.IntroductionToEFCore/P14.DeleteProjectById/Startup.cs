using System;
using System.IO;
using System.Linq;
using P02_DatabaseFirst.Data;

namespace P14.DeleteProjectById
{
    public class Startup
    {
        public static void Main()
        {
            using (var dbContext = new SoftUniContext())
            {
                var employeeProjects = dbContext
                    .EmployeesProjects
                    .Where(ep => ep.ProjectId == 2);

                dbContext
                    .EmployeesProjects
                    .RemoveRange(employeeProjects);

                var projectWithId2 = dbContext
                    .Projects.Find(2);

                dbContext
                    .Projects
                    .Remove(projectWithId2);

                dbContext.SaveChanges();

                var firstTenProjects = dbContext
                    .Projects
                    .Take(10)
                    .Select(p => p.Name);

                using (var sw = new StreamWriter("../../../ProjectNames.txt"))
                {
                    foreach (var projectName in firstTenProjects)
                    {
                        sw.WriteLine(projectName);
                        Console.WriteLine(projectName);
                    }
                }

            }            
        }
    }
}
