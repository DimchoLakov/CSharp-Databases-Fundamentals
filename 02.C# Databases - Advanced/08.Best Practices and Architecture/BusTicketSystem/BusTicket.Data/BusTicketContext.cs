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

        public virtual DbSet<BankAccount> BankAccounts { get; set; }
        public virtual DbSet<BusCompany> BusCompanies { get; set; }
        public virtual DbSet<BusStation> BusStations { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Town> Towns { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(DbContextConfig.ConnectionString);
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer(DbContextConfig.ConnectionString);
            //}
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
