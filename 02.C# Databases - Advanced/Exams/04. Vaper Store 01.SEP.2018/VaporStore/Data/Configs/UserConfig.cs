using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaporStore.Data.Models;

namespace VaporStore.Data.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasMany(x => x.Cards)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
