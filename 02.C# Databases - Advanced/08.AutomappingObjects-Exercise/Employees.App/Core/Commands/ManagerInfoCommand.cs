using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Employees.App.DTOs;
using Employees.Data;

namespace Employees.App.Core.Commands
{
    public class ManagerInfoCommand : Command
    {
        private const string NonExistingEmployee = "Employee with Id {0} does not exist!";
        private const string NotAManager = "Employee with Id {0} is not a manager!!";
        //private const string SuccessfullySetManager = "Employee with Id {0} has now manager with Id {1}";

        private readonly EmployeesDbContext _dbContext;

        public ManagerInfoCommand(IList<string> args, EmployeesDbContext dbContext) : base(args)
        {
            this._dbContext = dbContext;
        }

        public override string Execute()
        {
            int employeeId = int.Parse(this.Args[0]);

            //var manager = this._dbContext
            //    .Employees
            //    .FirstOrDefault(x => x.EmployeeId == employeeId);

            var manager = this._dbContext.Employees
                .ProjectTo<ManagerDto>()
                .SingleOrDefault(e => e.EmployeeId == employeeId);

            if (manager == null)
            {
                throw new ArgumentException(string.Format(NonExistingEmployee, employeeId));
            }

            if (manager.EmployeeId == 0)
            {
                throw new ArgumentException(string.Format(NotAManager, employeeId));   
            }
            
            return new string('-', 30) + Environment.NewLine + manager.ToString() + Environment.NewLine + new string('-', 30);
        }
    }
}
