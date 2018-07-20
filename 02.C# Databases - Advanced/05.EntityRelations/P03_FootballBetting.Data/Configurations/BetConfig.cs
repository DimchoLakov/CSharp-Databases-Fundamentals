using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class BetConfig : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.BetId);

            modelBuilder
                .Property(x => x.Amount)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.DateTime)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder
                .Property(x => x.Prediction)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(64);

            modelBuilder
                .Property(x => x.GameId)
                .IsRequired(true);

            modelBuilder
                .HasOne(x => x.Game)
                .WithMany(x => x.Bets)
                .HasForeignKey(x => x.GameId);

            modelBuilder
                .Property(x => x.UserId)
                .IsRequired(true);

            modelBuilder
                .HasOne(x => x.User)
                .WithMany(x => x.Bets)
                .HasForeignKey(x => x.UserId);
        }
    }
}
