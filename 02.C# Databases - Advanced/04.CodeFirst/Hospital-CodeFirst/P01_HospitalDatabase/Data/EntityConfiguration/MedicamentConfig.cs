using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    public class MedicamentConfig : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> modelBuilder)
        {
            modelBuilder
                .HasKey(p => p.MedicamentId);

            modelBuilder
                .Property(p => p.Name)
                .HasMaxLength(250)
                .IsUnicode(true);

            modelBuilder
                .HasMany(p => p.Prescriptions)
                .WithOne(p => p.Medicament)
                .HasForeignKey(p => p.MedicamentId);
        }
    }
}
