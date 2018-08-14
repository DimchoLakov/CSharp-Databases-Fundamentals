using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper.QueryableExtensions;
using Employees.App.DTOs;
using Employees.Data;

namespace Employees.App.Core.Commands
{
    public class ListEmployeesOlderThanCommand : Command
    {
        private readonly EmployeesDbContext _dbContext;

        public ListEmployeesOlderThanCommand(IList<string> args, EmployeesDbContext dbContext) : base(args)
        {
            this._dbContext = dbContext;
        }

        public override string Execute()
        {
            int age = int.Parse(this.Args[0]);

            var employees = this._dbContext
                .Employees
                .Where(e => e.Birthday != null)
                .Select(e => new
                {
                    Employee = AutoMapper.Mapper.Map<EmployeeDto>(e),
                    Manager = AutoMapper.Mapper.Map<ManagerDto>(e.Manager),
                    Age = Math.Ceiling((DateTime.Now - e.Birthday.Value).TotalDays / 365.2422),
                    e.Salary
                })
                .Where(e => e.Age > age)
                .OrderByDescending(e => e.Salary)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var e in employees)
            {
                var managerName = e.Manager == null ? "[no manager]" : e.Manager.LastName;
                var row = $"{e.Employee.FirstName} {e.Employee.LastName} - ${e.Salary:f2} - Manager: {managerName}";
                sb.AppendLine(row);
            }

            return new string('-', 30) + Environment.NewLine + sb.ToString().Trim() + Environment.NewLine + new string('-', 30);
        }
    }
}
