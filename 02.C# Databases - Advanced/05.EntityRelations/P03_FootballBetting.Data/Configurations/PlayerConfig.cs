using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class PlayerConfig : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.PlayerId);

            modelBuilder
                .Property(x => x.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(64);

            modelBuilder
                .Property(x => x.IsInjured)
                .IsRequired(true)
                .HasDefaultValue(false);

            modelBuilder
                .Property(x => x.SquadNumber)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.PositionId)
                .IsRequired(true);

            modelBuilder
                .HasOne(x => x.Position)
                .WithMany(x => x.Players)
                .HasForeignKey(x => x.PositionId);

            modelBuilder
                .Property(x => x.TeamId)
                .IsRequired(true);

            modelBuilder
                .HasOne(x => x.Team)
                .WithMany(x => x.Players)
                .HasForeignKey(x => x.TeamId);

            modelBuilder
                .HasMany(x => x.PlayerStatistics)
                .WithOne(x => x.Player)
                .HasForeignKey(x => x.PlayerId);
        }
    }
}
