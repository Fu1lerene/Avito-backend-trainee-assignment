using Avito.MerchStore.Domain.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace Avito.MerchStore.Infrastructure.Repositories;

public class MerchDbContext : DbContext
{
    public MerchDbContext(DbContextOptions<MerchDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<MerchItem> MerchItems { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<UserInventory> UserInventories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserInventory>()
            .HasKey(ui => new { ui.UserId, ui.ItemId });

        modelBuilder.Entity<UserInventory>()
            .HasOne(ui => ui.User)
            .WithMany(u => u.Inventory)
            .HasForeignKey(ui => ui.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserInventory>()
            .HasOne(ui => ui.Item)
            .WithMany(i => i.Inventory)
            .HasForeignKey(ui => ui.ItemId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<MerchItem>().HasData(
            new MerchItem { Id = 1, Name = "t-shirt", Price = 80 },
            new MerchItem { Id = 2, Name = "cup", Price = 20 },
            new MerchItem { Id = 3, Name = "book", Price = 50 },
            new MerchItem { Id = 4, Name = "pen", Price = 10 },
            new MerchItem { Id = 5, Name = "powerbank", Price = 200 },
            new MerchItem { Id = 6, Name = "hoody", Price = 300 },
            new MerchItem { Id = 7, Name = "umbrella", Price = 200 },
            new MerchItem { Id = 8, Name = "socks", Price = 10 },
            new MerchItem { Id = 9, Name = "wallet", Price = 50 },
            new MerchItem { Id = 10, Name = "pink-hoody", Price = 500 }
        );
    }
}