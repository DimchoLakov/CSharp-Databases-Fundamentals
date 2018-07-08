using System;
using System.Linq;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;

namespace P15.RemoveTowns
{
    public class Startup
    {
        public static void Main()
        {
            string townToDelete = Console.ReadLine();

            using (var dbContext = new SoftUniContext())
            {
                var addresses = dbContext
                    .Addresses
                    .Where(a => a.Town.Name == townToDelete);
                
                var town = dbContext
                    .Towns
                    .FirstOrDefault(t => t.Name == townToDelete);

                if (town == null)
                {
                    Console.WriteLine("Error! You are trying to delete non-existing town!");
                    Environment.Exit(1);
                }

                string tn = town.Name;
            
                int addressesCount = addresses.Count();

                var employees = dbContext
                    .Employees
                    .Where(e => e.Address.Town.Name == townToDelete);

                var c = employees.Count();

                foreach (Employee e in employees)
                {
                    e.AddressId = null;
                }

                dbContext
                    .Addresses.RemoveRange(addresses);

                dbContext
                    .Towns
                    .Remove(town);

                dbContext.SaveChanges();

                string addressAsStr = addressesCount > 1 ? "addresses" : "address";
                string wasWereAsStr = addressesCount > 1 ? "were" : "was";

                Console.WriteLine($"{addressesCount} {addressAsStr} in {townToDelete} {wasWereAsStr} deleted");
            }
        }
    }
}
