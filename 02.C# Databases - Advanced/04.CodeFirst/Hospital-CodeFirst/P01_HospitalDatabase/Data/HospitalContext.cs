using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.EntityConfiguration;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {

        }

        public HospitalContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientMedicament> Prescriptions { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //Make sure your ConnectionString is configured
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new DiagnoseConfig())
                .ApplyConfiguration(new MedicamentConfig())
                .ApplyConfiguration(new PatientConfig())
                .ApplyConfiguration(new PatientMedicamentConfig())
                .ApplyConfiguration(new VisitationConfig())
                .ApplyConfiguration(new DoctorConfig());
        }
    }
}
