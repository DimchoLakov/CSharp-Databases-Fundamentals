using System;
using System.Collections.Generic;
using System.Linq;
using Employees.Data;

namespace Employees.App.Core.Commands
{
    public class SetAddressCommand : Command
    {
        private const string NonExistingEmployee = "Employee with id {0} does not exist!";
        private const string SuccessfullySetAddress = "Employee's address with Id {0} was successfully set!";

        private readonly EmployeesDbContext _dbContext;

        public SetAddressCommand(IList<string> args, EmployeesDbContext dbContext) : base(args)
        {
            this._dbContext = dbContext;
        }

        public override string Execute()
        {
            int employeeId = int.Parse(this.Args[0]);

            var employee = this._dbContext
                .Employees
                .FirstOrDefault(e => e.EmployeeId == employeeId);

            if (employee == null)
            {
                throw new ArgumentException(string.Format(NonExistingEmployee, employeeId));
            }

            var address = this.Args.Skip(1).ToList();
            var fullAddress = string.Join(" ", address);

            employee.Address = fullAddress;

            this._dbContext.SaveChanges();

            return string.Format(SuccessfullySetAddress, employeeId);
        }
    }
}
