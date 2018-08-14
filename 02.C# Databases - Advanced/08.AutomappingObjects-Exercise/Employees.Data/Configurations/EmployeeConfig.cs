using Employees.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employees.Data.Configurations
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
                .HasKey(x => x.EmployeeId);

            builder
                .Property(x => x.FirstName)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(64);

            builder
                .Property(x => x.LastName)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(64);

            builder
                .Property(x => x.Salary)
                .IsRequired(true);

            builder
                .HasMany(x => x.ManagedEmployees)
                .WithOne(x => x.Manager)
                .HasForeignKey(x => x.ManagerId);
        }
    }
}
