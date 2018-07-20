using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class PositionConfig : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.PositionId);

            modelBuilder
                .Property(x => x.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(64);

            //modelBuilder                                  //already configurated
            //    .HasMany(x => x.Players)                  //already configurated
            //    .WithOne(x => x.Position)                 //already configurated
            //    .HasForeignKey(x => x.PositionId);        //already configurated
        }
    }
}
