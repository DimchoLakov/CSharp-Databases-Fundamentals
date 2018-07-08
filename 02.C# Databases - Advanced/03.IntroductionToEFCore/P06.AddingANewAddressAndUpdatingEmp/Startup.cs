using System;
using P02_DatabaseFirst.Data;
using System.Linq;
using P02_DatabaseFirst.Data.Models;

namespace P06.AddingANewAddressAndUpdatingEmp
{
    public class Startup
    {
        public static void Main()
        {
            var dbContext = new SoftUniContext();

            using (dbContext)
            {
                var newAddress = new Address()
                {
                    AddressText = "Vitoshka 15",
                    TownId = 4
                };

                var nakovEmployee = dbContext
                    .Employees
                    .FirstOrDefault(e => e.LastName == "Nakov");

                nakovEmployee.Address = newAddress;

                dbContext.SaveChanges();

                var firstTenAddresses = dbContext
                    .Employees
                    .OrderByDescending(e => e.AddressId)
                    .Select(e => e.Address.AddressText)
                    .Take(10);

                foreach (var address in firstTenAddresses)
                {
                    Console.WriteLine(address);
                }
            }
        }
    }
}
