using System.Collections.Generic;
using Employees.App.DTOs;
using Employees.Data;
using Employees.Models;
using AutoMapper;

namespace Employees.App.Core.Commands
{
    public class AddEmployeeCommand : Command
    {
        private const string SuccessfullyAddedEmployee = "Employee with name {0} {1} has been added successfully!";

        private readonly EmployeesDbContext _dbContext;

        public AddEmployeeCommand(IList<string> args, EmployeesDbContext dbContext) : base(args)
        {
            this._dbContext = dbContext;
        }

        public override string Execute()
        {
            string firstName = this.Args[0];
            string lastName = this.Args[1];
            decimal salary = decimal.Parse(this.Args[2]);

            EmployeeDto employeeDto = new EmployeeDto()
            {
                FirstName = firstName,
                LastName = lastName,
                Salary = salary
            };

            Employee employee = AutoMapper.Mapper.Map<Employee>(employeeDto);

            this._dbContext.Employees.Add(employee);
            this._dbContext.SaveChanges();

            return string.Format(SuccessfullyAddedEmployee, firstName, lastName);
        }
    }
}
