using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaporStore.Data.Models;

namespace VaporStore.Data.Configs
{
    public class GameConfig : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder
                .HasOne(x => x.Developer)
                .WithMany(x => x.Games)
                .HasForeignKey(x => x.DeveloperId);

            builder
                .HasOne(x => x.Genre)
                .WithMany(x => x.Games)
                .HasForeignKey(x => x.GenreId);

            builder
                .HasMany(x => x.Purchases)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId);
        }
    }
}
