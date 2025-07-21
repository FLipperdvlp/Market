using Market.Entities;
using Microsoft.EntityFrameworkCore;

namespace Market.Persistence;

public class MarketContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<UserProduct> UserProducts { get; set; }

    public MarketContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connectionString:"Data Source=market.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Phone 
        modelBuilder.Entity<User>().HasIndex(u => u.Phone).IsUnique();
        modelBuilder.Entity<User>().Property(u => u.Phone)
            .IsRequired()
            .HasMaxLength(13);
        
        
        //Email
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(64);
        
        
        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
        
        
        
        //Product name
        modelBuilder.Entity<Product>().Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(64);
        
        
        modelBuilder.Entity<UserProduct>()
            .HasOne(up => up.User)
            .WithMany(user => user.UserProduct)
            .HasForeignKey(up => up.UserId);

        modelBuilder.Entity<UserProduct>()
            .HasOne(up => up.Product)
            .WithMany(product => product.UserProduct)
            .HasForeignKey(up => up.ProductId);


        modelBuilder.Entity<UserProduct>().HasKey(up => new { up.UserId, up.ProductId });
    }
}