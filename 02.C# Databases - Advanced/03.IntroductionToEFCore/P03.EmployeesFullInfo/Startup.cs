using System;
using System.Linq;
using P02_DatabaseFirst.Data;

namespace P03.EmployeesFullInfo
{
    public class Startup
    {
        public static void Main()
        {
            var dbContext = new SoftUniContext();

            using (dbContext)
            {
                var employees = dbContext
                    .Employees
                    .OrderBy(e => e.EmployeeId)
                    .Select
                    (
                        e => string.Format($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}")
                    );

                employees.ToList().ForEach(Console.WriteLine);
            }
        }
    }
}
