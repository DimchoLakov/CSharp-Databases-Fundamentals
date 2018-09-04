using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaporStore.Data.Models;

namespace VaporStore.Data.Configs
{
    public class GameTagConfig : IEntityTypeConfiguration<GameTag>
    {
        public void Configure(EntityTypeBuilder<GameTag> builder)
        {
            builder
                .HasKey(x => new { x.GameId, x.TagId });

            builder
                .HasOne(x => x.Tag)
                .WithMany(x => x.GameTags)
                .HasForeignKey(x => x.TagId);

            builder
                .HasOne(x => x.Game)
                .WithMany(x => x.GameTags)
                .HasForeignKey(x => x.GameId);
        }
    }
}
