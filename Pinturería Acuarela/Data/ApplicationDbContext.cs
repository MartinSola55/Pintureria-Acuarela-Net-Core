using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pintureria_Acuarela.Data.Seeding;
using Pintureria_Acuarela.Models;

namespace Pintureria_Acuarela.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones adicionales aquí

            new DbInitializer(modelBuilder).Seed();

            modelBuilder.Entity<ProductBusiness>()
                .HasKey(pb => new { pb.ProductID, pb.BusinessID, pb.CreatedAt});
            modelBuilder.Entity<ProductOrder>()
                .HasKey(po => new { po.ProductID, po.OrderID });
            modelBuilder.Entity<ProductSale>()
                .HasKey(ps => new { ps.ProductID, ps.SaleID });

            modelBuilder.Entity<Order>()
                .HasQueryFilter(x => x.DeletedAt == null);
            modelBuilder.Entity<Product>()
                .HasQueryFilter(x => x.DeletedAt == null);
            modelBuilder.Entity<ProductBusiness>()
                .HasQueryFilter(x => x.DeletedAt == null);
            modelBuilder.Entity<ProductSale>()
                .HasQueryFilter(x => x.DeletedAt == null);
            modelBuilder.Entity<Sale>()
                .HasQueryFilter(x => x.DeletedAt == null);
        }

        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Capacity> Capacities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBusiness> ProductsBusiness { get; set; }
        public DbSet<ProductOrder> ProductsOrder { get; set; }
        public DbSet<ProductSale> ProductsSale { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
    }
}