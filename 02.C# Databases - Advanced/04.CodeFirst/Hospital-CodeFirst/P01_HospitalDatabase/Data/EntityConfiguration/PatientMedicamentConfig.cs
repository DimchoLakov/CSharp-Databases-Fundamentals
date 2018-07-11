using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    public class PatientMedicamentConfig : IEntityTypeConfiguration<PatientMedicament>
    {
        public void Configure(EntityTypeBuilder<PatientMedicament> modelBuilder)
        {
            modelBuilder
                .HasKey(pm => new { pm.PatientId, pm.MedicamentId });

            //modelBuilder
            //    .HasOne(p => p.Patient)
            //    .WithMany(p => p.Prescriptions)
            //    .HasForeignKey(p => p.PatientId);
        }
    }
}
