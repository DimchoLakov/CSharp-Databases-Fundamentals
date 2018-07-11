using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    public class DiagnoseConfig : IEntityTypeConfiguration<Diagnose>
    {
        public void Configure(EntityTypeBuilder<Diagnose> modelBuilder)
        {
            modelBuilder
                .HasKey(d => d.DiagnoseId);

            modelBuilder
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsUnicode(true);

            modelBuilder
                .Property(p => p.Comments)
                .HasMaxLength(250)
                .IsUnicode(true);
        }
    }
}
