namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.StudentId);

            modelBuilder
                .Property(x => x.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(100);

            modelBuilder
                .Property(x => x.PhoneNumber)
                .IsRequired(false)
                .IsUnicode(false)
                .HasMaxLength(10);

            modelBuilder
                .Property(x => x.RegisteredOn)
                .IsRequired(true)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder
                .Property(x => x.Birthday)
                .IsRequired(false);
        }
    }
}
