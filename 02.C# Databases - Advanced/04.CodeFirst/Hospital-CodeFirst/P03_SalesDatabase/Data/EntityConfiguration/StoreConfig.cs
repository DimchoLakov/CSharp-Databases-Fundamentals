using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data.EntityConfiguration
{
    public class StoreConfig : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> modelBuilder)
        {
            modelBuilder
                .HasKey(p => p.StoreId);

            modelBuilder
                .Property(p => p.Name)
                .IsRequired(true)
                .HasMaxLength(80)
                .IsUnicode(true);
        }
    }
}
