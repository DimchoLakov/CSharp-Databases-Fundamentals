using System;
using System.IO;
using System.Linq;
using P02_DatabaseFirst.Data;

namespace P10.DepartmentsWithMoreThan5Employees
{
    public class Startup
    {
        public static void Main()
        {
            var dbContext = new SoftUniContext();

            var departments = dbContext
                .Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name)
                .Select(nd => new
                {
                    DepartmentName = nd.Name,
                    ManFirstName = nd.Manager.FirstName,
                    ManLastName = nd.Manager.LastName,
                    Employees = nd.Employees.Select(e => new
                        {
                            EmpFirstName = e.FirstName,
                            EmpLastName = e.LastName,
                            EmpJobTitle = e.JobTitle
                        })
                        .OrderBy(e => e.EmpFirstName)
                        .ThenBy(e => e.EmpLastName)
                });

            using (dbContext)
            {
                using (StreamWriter sw = new StreamWriter("../../../DepartmentsInfo.txt"))
                {
                    foreach (var d in departments)
                    {
                        string departmentInfo = $"{d.DepartmentName} - {d.ManFirstName} {d.ManLastName}";

                        Console.WriteLine(departmentInfo);
                        sw.WriteLine(departmentInfo);

                        foreach (var e in d.Employees)
                        {
                            string employeeInfo = $"{e.EmpFirstName} {e.EmpLastName} - {e.EmpJobTitle}";

                            Console.WriteLine(employeeInfo);
                            sw.WriteLine(employeeInfo);
                        }

                        string dashes = new string('-', 10);

                        Console.WriteLine(dashes);
                        sw.WriteLine(dashes);
                    }
                }
            }
        }
    }
}