using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class TownConfig : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.TownId);

            modelBuilder
                .Property(x => x.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(64);

            modelBuilder
                .Property(x => x.CountryId)
                .IsRequired(true);
            
            modelBuilder
                .HasMany(x => x.Teams)
                .WithOne(x => x.Town)
                .HasForeignKey(x => x.TownId);

            //modelBuilder                                 //configured in CountryConfig
            //    .HasOne(x => x.Country)                  //configured in CountryConfig
            //    .WithMany(x => x.Towns)                  //configured in CountryConfig
            //    .HasForeignKey(x => x.CountryId);        //configured in CountryConfig
        }
    }
}
