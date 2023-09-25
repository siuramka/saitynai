using BackendApi.Auth.Models;
using BackendApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Data;

public class ShopDbContext : DbContext// IdentityDbContext<ShopUser>
{
    private readonly IConfiguration _configuration;
    
    public ShopDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Subscription>()
        //     .HasOne(s => s.ShopUser)
        //     .WithMany(u => u.Subscriptions)
        //     .HasForeignKey(s => s.ShopUserIdId)
        //     .OnDelete(DeleteBehavior.Restrict); // Specify NO ACTION

        modelBuilder.Entity<Software>()
            .HasOne(s => s.Shop)
            .WithMany(sh => sh.Softwares)
            .HasForeignKey(s => s.ShopId)
            .OnDelete(DeleteBehavior.Restrict); // Specify NO ACTION
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetValue<string>("ConnectionString:DockerSqlServer"));
    }
    
    public DbSet<ShopUser> ShopUsers { get; }
    public DbSet<Shop> Shops { get; }
    public DbSet<Software> Softwares { get; }
    public DbSet<Subscription> Subscriptions { get; }
}