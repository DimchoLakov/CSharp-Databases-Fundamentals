using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;

    public class PatientConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> modelBuilder)
        {
            modelBuilder
                .HasKey(i => i.PatientId);

            modelBuilder
                .Property(p => p.FirstName)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsUnicode(true);

            modelBuilder
                .Property(p => p.LastName)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsUnicode(true);

            modelBuilder
                .Property(p => p.Address)
                .IsRequired(true)
                .HasMaxLength(250)
                .IsUnicode(true);

            modelBuilder
                .Property(p => p.Email)
                .IsRequired(true)
                .HasMaxLength(80)
                .IsUnicode(false);

            modelBuilder
                .HasAlternateKey(e => e.Email)
                .HasName("AlternateKey_Email");

            modelBuilder
                .Property(p => p.HasInsurance)
                .IsRequired(true)
                .HasDefaultValue(true);

            modelBuilder
                .HasMany(p => p.Prescriptions)
                .WithOne(p => p.Patient)
                .HasForeignKey(p => p.PatientId);

            modelBuilder
                .HasMany(p => p.Visitations)
                .WithOne(p => p.Patient)
                .HasForeignKey(p => p.PatientId);

            modelBuilder
                .HasMany(p => p.Diagnoses)
                .WithOne(p => p.Patient)
                .HasForeignKey(p => p.PatientId);
        }
    }
}
