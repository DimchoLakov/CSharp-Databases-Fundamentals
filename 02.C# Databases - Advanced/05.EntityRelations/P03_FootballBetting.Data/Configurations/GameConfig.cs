using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class GameConfig : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.GameId);

            modelBuilder
                .Property(x => x.AwayTeamBetRate)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.AwayTeamGoals)
                .IsRequired(true)
                .HasDefaultValue(0);

            modelBuilder
                .Property(x => x.AwayTeamId)
                .IsRequired(true);

            modelBuilder
                .HasOne(x => x.AwayTeam)
                .WithMany(x => x.AwayGames)
                .HasForeignKey(x => x.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Property(x => x.DrawBetRate)
                .IsRequired(true)
                .HasDefaultValue(0);

            modelBuilder
                .Property(x => x.HomeTeamBetRate)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.HomeTeamGoals)
                .IsRequired(true)
                .HasDefaultValue(0);

            modelBuilder
                .Property(x => x.HomeTeamId)
                .IsRequired(true);

            modelBuilder
                .HasOne(x => x.HomeTeam)
                .WithMany(x => x.HomeGames)
                .HasForeignKey(x => x.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Property(x => x.Result)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(256);

            modelBuilder
                .Property(x => x.DateTime)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder
                .HasMany(x => x.PlayerStatistics)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId);
        }
    }
}
