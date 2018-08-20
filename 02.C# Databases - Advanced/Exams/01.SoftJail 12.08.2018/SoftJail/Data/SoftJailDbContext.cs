using SoftJail.Data.Configurations;
using SoftJail.Data.Models;

namespace SoftJail.Data
{
	using Microsoft.EntityFrameworkCore;

	public class SoftJailDbContext : DbContext
	{
		public SoftJailDbContext()
		{
		}

		public SoftJailDbContext(DbContextOptions options)
			: base(options)
		{
		}

	    public DbSet<Cell> Cells { get; set; }
	    public DbSet<Department> Departments { get; set; }
	    public DbSet<Mail> Mails { get; set; }
	    public DbSet<Officer> Officers { get; set; }
	    public DbSet<Prisoner> Prisoners { get; set; }
	    public DbSet<OfficerPrisoner> OfficersPrisoners { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder
					.UseSqlServer(Configuration.ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
            builder
                .ApplyConfiguration(new CellConfig())
                .ApplyConfiguration(new DepartmentConfig())
                .ApplyConfiguration(new MailConfig())
                .ApplyConfiguration(new OfficerConfig())
                .ApplyConfiguration(new OfficerPrisonerConfig())
                .ApplyConfiguration(new PrisonerConfig());
        }
	}
}