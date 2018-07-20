namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class HomeworkConfig : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.HomeworkId);

            modelBuilder
                .Property(x => x.Content)
                .IsRequired(true)
                .IsUnicode(false);

            modelBuilder
                .Property(x => x.ContentType)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.SubmissionTime)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.StudentId)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.CourseId)
                .IsRequired(true);

            modelBuilder
                .HasOne(x => x.Student)
                .WithMany(x => x.HomeworkSubmissions)
                .HasForeignKey(x => x.StudentId);

            modelBuilder
                .HasOne(x => x.Course)
                .WithMany(x => x.HomeworkSubmissions)
                .HasForeignKey(x => x.CourseId);
        }
    }
}
