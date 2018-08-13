using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using ProductShop.Data.Configurations;
using ProductShop.Models;

namespace ProductShop.Data
{
    public class ProductShopDbContext : DbContext
    {
        public ProductShopDbContext()
        {
        }

        public ProductShopDbContext(DbContextOptions<ProductShopDbContext> options) : base(options)
        {    
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbContextConfig.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new CategoryConfig())
                .ApplyConfiguration(new CategoryProductConfig())
                .ApplyConfiguration(new ProductConfig())
                .ApplyConfiguration(new UserConfig());
        }
    }
}
