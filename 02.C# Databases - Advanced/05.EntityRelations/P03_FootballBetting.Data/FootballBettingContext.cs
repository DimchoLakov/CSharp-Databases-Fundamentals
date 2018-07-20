using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data.Configurations;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext()
        {
        }

        public FootballBettingContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Bet> Bets { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Town> Towns { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbContextConfiguration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new BetConfig())
                .ApplyConfiguration(new ColorConfig())
                .ApplyConfiguration(new CountryConfig())
                .ApplyConfiguration(new GameConfig())
                .ApplyConfiguration(new PlayerConfig())
                .ApplyConfiguration(new PlayerStatisticConfig())
                .ApplyConfiguration(new PositionConfig())
                .ApplyConfiguration(new TeamConfig())
                .ApplyConfiguration(new TownConfig())
                .ApplyConfiguration(new UserConfig());
        }
    }
}
