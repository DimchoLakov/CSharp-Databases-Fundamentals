namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.CourseId);

            modelBuilder
                .Property(x => x.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(80);

            modelBuilder
                .Property(x => x.Description)
                .IsRequired(false)
                .IsUnicode(true);

            modelBuilder
                .Property(x => x.StartDate)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.EndDate)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.Price)
                .IsRequired(true);
        }
    }
}
