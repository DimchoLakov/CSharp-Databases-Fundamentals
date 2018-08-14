using System;
using System.Collections.Generic;

namespace Employees.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public DateTime? Birthday { get; set; }

        public string Address { get; set; }

        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }

        public List<Employee> ManagedEmployees { get; set; }

        public override string ToString()
        {
            var result = $"{this.EmployeeId} - {this.FirstName} {this.LastName} - ${this.Salary:f2}{Environment.NewLine}";
            result += $"Birthday: {this.Birthday:d}{Environment.NewLine}";
            result += $"Address: {this.Address}";

            return result;
        }
    }
}
