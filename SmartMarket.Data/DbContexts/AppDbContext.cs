using Microsoft.EntityFrameworkCore;
using SmartMarket.Domin.Entities.Cards;
using SmartMarket.Domin.Entities.Categories;
using SmartMarket.Domin.Entities.CencelOrders;
using SmartMarket.Domin.Entities.Partners;
using SmartMarket.Domin.Entities.Products;
using SmartMarket.Domin.Entities.Users;

namespace SmartMarket.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    public DbSet<Card> Cards { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Partner> Partners { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CencelOrder> CancelOrders { get; set; }
    public DbSet<PartnerProduct> PartnerProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationship between User and Product
        modelBuilder.Entity<Product>()
            .HasOne(p => p.User)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.UserId);

        // Configure relationship between PartnerProduct and Partner
        modelBuilder.Entity<PartnerProduct>()
            .HasOne(pp => pp.Partner)
            .WithMany(p => p.PartnerProducts)
            .HasForeignKey(pp => pp.PartnerId);

        // Configure relationship between PartnerProduct and User
        modelBuilder.Entity<PartnerProduct>()
            .HasOne(pp => pp.User)
            .WithMany(u => u.PartnerProducts)
            .HasForeignKey(pp => pp.UserId);

        // Configure relationship between Product and Category
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        // Configure relationship between PartnerProduct and Category
        modelBuilder.Entity<PartnerProduct>()
            .HasOne(pp => pp.Category)
            .WithMany(c => c.PartnersProducts)
            .HasForeignKey(pp => pp.CategoryId);

        // Configure relationship between Card and Category
        modelBuilder.Entity<Card>()
            .HasOne(c => c.Category)
            .WithMany(c => c.Cards)
            .HasForeignKey(c => c.CategoryId);
    }

}
