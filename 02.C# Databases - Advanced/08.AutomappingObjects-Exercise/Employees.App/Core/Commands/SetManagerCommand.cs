using System;
using System.Collections.Generic;
using System.Linq;
using Employees.Data;

namespace Employees.App.Core.Commands
{
    public class SetManagerCommand : Command
    {
        private const string NonExistingEmployee = "Employee with Id {0} does not exist!";
        private const string SuccessfullySetManager = "Employee with Id {0} has now manager with Id {1}";
            
        private readonly EmployeesDbContext _dbContext;

        public SetManagerCommand(IList<string> args, EmployeesDbContext dbContext) : base(args)
        {
            this._dbContext = dbContext;
        }

        public override string Execute()
        {
            int firstEmlpoyeeId = int.Parse(this.Args[0]);
            int secondEmployeeId = int.Parse(this.Args[1]);
            
            var firstEmployee = this._dbContext
                .Employees
                .FirstOrDefault(x => x.EmployeeId == firstEmlpoyeeId);

            var secondEmlpoyee = this._dbContext
                .Employees
                .FirstOrDefault(x => x.EmployeeId == secondEmployeeId);

            if (firstEmployee == null)
            {
                throw new ArgumentException(string.Format(NonExistingEmployee, firstEmlpoyeeId));
            }

            if (secondEmlpoyee == null)
            {
                throw new ArgumentException(string.Format(NonExistingEmployee, secondEmployeeId));
            }

            firstEmployee.ManagerId = secondEmlpoyee.EmployeeId;
            firstEmployee.Manager = secondEmlpoyee;

            this._dbContext.SaveChanges();

            return string.Format(SuccessfullySetManager, firstEmlpoyeeId, secondEmployeeId);
        }
    }
}
