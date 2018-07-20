using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class ColorConfig : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.ColorId);

            modelBuilder
                .Property(x => x.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(64);
            
            //modelBuilder                                     //configured in TeamConfig
            //    .HasMany(x => x.PrimaryKitTeams)             //configured in TeamConfig
            //    .WithOne(x => x.PrimaryKitColor)             //configured in TeamConfig
            //    .HasForeignKey(x => x.PrimaryKitColorId)     //configured in TeamConfig
            //    .OnDelete(DeleteBehavior.Restrict);          //configured in TeamConfig

            //modelBuilder                                     //configured in TeamConfig
            //    .HasMany(x => x.SecondaryKitTeams)           //configured in TeamConfig
            //    .WithOne(x => x.SecondaryKitColor)           //configured in TeamConfig
            //    .HasForeignKey(x => x.SecondaryKitColorId)   //configured in TeamConfig
            //    .OnDelete(DeleteBehavior.Restrict);          //configured in TeamConfig
        }
    }
}
