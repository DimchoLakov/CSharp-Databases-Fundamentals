using P03_FootballBetting.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P03_FootballBetting.Data.Configurations
{
    public class PlayerStatisticConfig : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> modelBuilder)
        {
            modelBuilder
                .HasKey(x => new { x.PlayerId, x.GameId });

            modelBuilder
                .Property(x => x.Assists)
                .IsRequired(true)
                .HasDefaultValue(0);

            modelBuilder
                .Property(x => x.MinutesPlayed)
                .IsRequired(true)
                .HasDefaultValue(0);

            modelBuilder
                .Property(x => x.ScoredGoals)
                .IsRequired(true)
                .HasDefaultValue(0);

            //modelBuilder
            //    .HasOne(x => x.Game)                     //already configurated
            //    .WithMany(x => x.PlayerStatistics)       //already configurated
            //    .HasForeignKey(x => x.GameId);           //already configurated
            //                                             //already configurated
            //modelBuilder                                 //already configurated
            //    .HasOne(x => x.Player)                   //already configurated
            //    .WithMany(x => x.PlayerStatistics)       //already configurated
            //    .HasForeignKey(x => x.PlayerId);         //already configurated
        }
    }
}
