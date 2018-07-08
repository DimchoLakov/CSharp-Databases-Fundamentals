using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using P02_DatabaseFirst.Data;

namespace P08.AddressesByTown
{
    public class Startup
    {
        public static void Main()
        {
            var dbContext = new SoftUniContext();

            using (dbContext)
            {
                var addresses = dbContext
                    .Addresses
                    .OrderByDescending(a => a.Employees.Count)
                    .ThenBy(a => a.Town.Name)
                    .ThenBy(a => a.AddressText)
                    .Take(10)
                    .Select(a => new
                    {
                        AddressText = a.AddressText,
                        TownName = a.Town.Name,
                        EmployeesCount = a.Employees.Count
                    });

                addresses.ToList().ForEach(a => Console.WriteLine($"{a.AddressText}, {a.TownName} - {a.EmployeesCount} employees"));
            }
        }
    }
}
