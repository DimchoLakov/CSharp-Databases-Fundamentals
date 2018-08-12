using BusTicket.Data.Configurations;
using BusTicket.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTicket.Data
{
    public class BusTicketContext : DbContext
    {
        public BusTicketContext()
        {
        }

        public BusTicketContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BusCompany> BusCompanies { get; set; }
        public DbSet<BusStation> BusStations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Trip> Trips { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbContextConfig.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new BankAccountConfig())
                .ApplyConfiguration(new BusCompanyConfig())
                .ApplyConfiguration(new BusStationConfig())
                .ApplyConfiguration(new CustomerConfig())
                .ApplyConfiguration(new ReviewConfig())
                .ApplyConfiguration(new TicketConfig())
                .ApplyConfiguration(new TownConfig())
                .ApplyConfiguration(new TripConfig());
        }
    }
}
