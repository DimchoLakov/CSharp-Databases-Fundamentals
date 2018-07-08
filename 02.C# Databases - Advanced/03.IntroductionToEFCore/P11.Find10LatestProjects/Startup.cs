using System;
using System.Globalization;
using System.IO;
using System.Linq;
using P02_DatabaseFirst.Data;

namespace P11.Find10LatestProjects
{
    public class Startup
    {
        public static void Main()
        {
            using (var dbContext = new SoftUniContext())
            {
                var projects = dbContext
                    .Projects
                    .OrderByDescending(p => p.StartDate)
                    .Take(10)
                    .OrderBy(p => p.Name)
                    .Select(p => new
                    {
                        Name = p.Name,
                        Description = p.Description,
                        StartDate = p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                    });

                using (var sw = new StreamWriter("../../../Projects.txt"))
                {
                    foreach (var p in projects)
                    {
                        Console.WriteLine(p.Name);
                        Console.WriteLine(p.Description);
                        Console.WriteLine(p.StartDate);

                        sw.WriteLine(p.Name);
                        sw.WriteLine(p.Description);
                        sw.WriteLine(p.StartDate);
                    }
                }
            }
        }
    }
}
