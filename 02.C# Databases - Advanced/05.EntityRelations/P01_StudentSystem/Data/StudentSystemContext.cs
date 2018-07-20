namespace P01_StudentSystem.Data
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Configurations;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {
        }

        public StudentSystemContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Homework> HomeworkSubmissions { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbContextConfiguration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .ApplyConfiguration(new CourseConfig())
                .ApplyConfiguration(new HomeworkConfig())
                .ApplyConfiguration(new ResourceConfig())
                .ApplyConfiguration(new StudentConfig())
                .ApplyConfiguration(new StudentConfig())
                .ApplyConfiguration(new StudentCourseConfig());
        }
    }
}
