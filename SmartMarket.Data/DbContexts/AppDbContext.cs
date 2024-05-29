﻿using Microsoft.EntityFrameworkCore;
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

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasOne(c => c.Category)
                .WithMany()
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.Yiguvchi)
                .WithMany(u => u.YiguvchiCards)
                .HasForeignKey(c => c.YukYiguvchId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.Casher)
                .WithMany(u => u.CasherCards)
                .HasForeignKey(c => c.CasherId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasMany(u => u.YiguvchiCards)
                .WithOne(c => c.Yiguvchi)
                .HasForeignKey(c => c.YukYiguvchId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(u => u.CasherCards)
                .WithOne(c => c.Casher)
                .HasForeignKey(c => c.CasherId)
                .OnDelete(DeleteBehavior.Restrict);
        });

    }
}
