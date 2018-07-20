using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;
using System.Linq;

namespace P03_FootballBetting.Data.Configurations
{
    public class TeamConfig : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.TeamId);

            modelBuilder
                .Property(x => x.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(64);

            modelBuilder
                .Property(x => x.Budget)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.TownId)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.PrimaryKitColorId)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.SecondaryKitColorId)
                .IsRequired(true);

            modelBuilder
                .HasOne(x => x.PrimaryKitColor)
                .WithMany(x => x.PrimaryKitTeams)
                .HasForeignKey(x => x.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .HasOne(x => x.SecondaryKitColor)
                .WithMany(x => x.SecondaryKitTeams)
                .HasForeignKey(x => x.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder                                          //configured in GameConfig
            //    .HasMany(x => x.AwayGames)                        //configured in GameConfig
            //    .WithOne(x => x.AwayTeam)                         //configured in GameConfig
            //    .HasForeignKey(x => x.AwayTeamId);                //configured in GameConfig

            //modelBuilder                                          //configured in GameConfig
            //    .HasMany(x => x.HomeGames)                        //configured in GameConfig
            //    .WithOne(x => x.HomeTeam)                         //configured in GameConfig
            //    .HasForeignKey(x => x.HomeTeamId);                //configured in GameConfig

            //modelBuilder                                          //configured in TownConfig
            //    .HasOne(x => x.Town)                              //configured in TownConfig
            //    .WithMany(x => x.Teams)                           //configured in TownConfig
            //    .HasForeignKey(x => x.TownId);                    //configured in TownConfig
        }
    }
}
