using BackendApi.Auth.Models;
using BackendApi.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Data;

public class ShopDbContext : IdentityDbContext<ShopUser>
{
    private readonly IConfiguration _configuration;
    
    public ShopDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.ShopUser)
            .WithMany(u => u.Subscriptions)
            .HasForeignKey(s => s.ShopUserId)
            .OnDelete(DeleteBehavior.NoAction); 

        modelBuilder.Entity<Software>()
            .HasOne(s => s.Shop)
            .WithMany(sh => sh.Softwares)
            .HasForeignKey(s => s.ShopId)
            .OnDelete(DeleteBehavior.NoAction);
        
        base.OnModelCreating(modelBuilder);
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetValue<string>("ConnectionString:DockerSqlServer"));
    }
    
    // public DbSet<ShopUser> ShopUsers { get; }
    public DbSet<Shop> Shops { get; }
    public DbSet<Software> Softwares { get; }
    public DbSet<Subscription> Subscriptions { get; }
}