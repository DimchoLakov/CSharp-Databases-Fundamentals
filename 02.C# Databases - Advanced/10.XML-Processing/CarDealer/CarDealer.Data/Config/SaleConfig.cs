using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDealer.Data.Config
{
    public class SaleConfig : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder
                .HasKey(x => x.SaleId);

            builder
                .HasOne(x => x.Car)
                .WithMany(x => x.Sales)
                .HasForeignKey(x => x.CarId);

            builder
                .HasOne(x => x.Customer)
                .WithMany(x => x.Sales)
                .HasForeignKey(x => x.CustomerId);
        }
    }
}
