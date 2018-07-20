using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.UserId);

            modelBuilder
                .Property(x => x.Balance)
                .IsRequired(true)
                .HasDefaultValue(0);

            modelBuilder
                .Property(x => x.Email)
                .IsRequired(true)
                .IsUnicode(false)
                .HasMaxLength(64);

            modelBuilder
                .Property(x => x.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(64);

            modelBuilder
                .Property(x => x.Username)
                .IsRequired(true)
                .IsUnicode(false)
                .HasMaxLength(64);

            modelBuilder
                .Property(x => x.Password)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(32);

            //modelBuilder
            //    .HasMany(x => x.Bets)                  //configured in BetConfig
            //    .WithOne(x => x.User)                  //configured in BetConfig
            //    .HasForeignKey(x => x.UserId);         //configured in BetConfig
        }
    }
}
