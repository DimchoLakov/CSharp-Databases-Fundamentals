using System;
using System.Collections.Generic;
using System.Linq;
using Employees.App.DTOs;
using Employees.Data;

namespace Employees.App.Core.Commands
{
    public class EmployeeInfoCommand : Command
    {
        private const string NonExistingEmployee = "Employee with id {0} does not exist!";

        private readonly EmployeesDbContext _dbContext;

        public EmployeeInfoCommand(IList<string> args, EmployeesDbContext dbContext) : base(args)
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

            var employeeDto = AutoMapper.Mapper.Map<EmployeeDto>(employee);

            return employeeDto.ToString();
        }
    }
}
