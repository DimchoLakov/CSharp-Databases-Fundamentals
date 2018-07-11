using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data.EntityConfiguration
{
    public class SaleConfig : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> modelBuilder)
        {
            modelBuilder
                .HasKey(p => p.SaleId);

            modelBuilder
                .Property(p => p.Date)
                .IsRequired(true)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder
                .Property(p => p.ProductId)
                .IsRequired(true);

            modelBuilder
                .Property(p => p.CustomerId)
                .IsRequired(true);

            modelBuilder
                .Property(p => p.StoreId)
                .IsRequired(true);

            modelBuilder
                .HasOne(p => p.Product)
                .WithMany(p => p.Sales)
                .HasForeignKey(p => p.ProductId);


            modelBuilder
                .HasOne(p => p.Customer)
                .WithMany(p => p.Sales)
                .HasForeignKey(p => p.CustomerId);

            modelBuilder
                .HasOne(p => p.Store)
                .WithMany(p => p.Sales)
                .HasForeignKey(p => p.StoreId);
        }
    }
}
