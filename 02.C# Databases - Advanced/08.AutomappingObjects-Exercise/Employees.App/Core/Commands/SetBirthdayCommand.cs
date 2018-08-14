using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Employees.Data;

namespace Employees.App.Core.Commands
{
    public class SetBirthdayCommand : Command
    {
        private const string NonExistingEmployee = "Employee with id {0} does not exist!";
        private const string SuccessfullySetBirthday = "Employee's birthday with Id {0} was successfully set!";

        private readonly EmployeesDbContext _dbContext;

        public SetBirthdayCommand(IList<string> args, EmployeesDbContext dbContext) : base(args)
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

            var birthday = DateTime.ParseExact(this.Args[1], "dd-MM-yyyy", CultureInfo.InvariantCulture);

            employee.Birthday = birthday;

            this._dbContext.SaveChanges();

            return string.Format(SuccessfullySetBirthday, employeeId);
        }
    }
}
