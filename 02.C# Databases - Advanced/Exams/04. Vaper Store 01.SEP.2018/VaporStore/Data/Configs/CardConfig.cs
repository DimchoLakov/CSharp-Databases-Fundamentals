using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaporStore.Data.Models;

namespace VaporStore.Data.Configs
{
    public class CardConfig : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder
                .HasMany(x => x.Purchases)
                .WithOne(x => x.Card)
                .HasForeignKey(x => x.CardId);
        }
    }
}
