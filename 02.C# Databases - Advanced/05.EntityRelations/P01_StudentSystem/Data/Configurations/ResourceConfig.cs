namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class ResourceConfig : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.ResourceId);

            modelBuilder
                .Property(x => x.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

            modelBuilder
                .Property(x => x.Url)
                .IsRequired(true)
                .IsUnicode(false);

            modelBuilder
                .Property(x => x.ResourceType)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.CourseId)
                .IsRequired(true);

            modelBuilder
                .HasOne(x => x.Course)
                .WithMany(x => x.Resources)
                .HasForeignKey(x => x.CourseId);
        }
    }
}
