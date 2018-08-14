using System.Collections.Generic;
using System.Text;
using Employees.Models;

namespace Employees.App.DTOs
{
    public class ManagerDto
    {
        //public ManagerDto()
        //{
        //    this.Employees = new List<Employee>();
        //}

        //public ManagerDto(List<EmployeeDto> employees)
        //{
        //    this.Employees = new List<Employee>(employees);
        //}

        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Employee> ManagedEmployees { get; set; }

        public int EmployeesCount => this.ManagedEmployees.Count;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.FirstName} {this.LastName} | Employees: {this.EmployeesCount}");

            foreach (var employee in this.ManagedEmployees)
            {
                var employeeDto = AutoMapper.Mapper.Map<EmployeeDto>(employee);

                sb.AppendLine($"    - {employeeDto.FirstName} {employeeDto.LastName} - ${employeeDto.Salary:f2}");
            }

            return sb.ToString().Trim();
        }
    }
}
