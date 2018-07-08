using System;
using System.Linq;
using P02_DatabaseFirst.Data;

namespace P04.EmpsWithSalaryOver50000
{
    public class Startup
    {
        public static void Main()
        {
            var dbContext = new SoftUniContext();

            using (dbContext)
            {
                var employeeNames = dbContext
                    .Employees
                    .Where(e => e.Salary > 50000)
                    .Select(e => e.FirstName)
                    .OrderBy(e => e);

                employeeNames.ToList().ForEach(Console.WriteLine);
            }
        }
    }
}
