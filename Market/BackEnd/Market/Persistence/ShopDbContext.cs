using Microsoft.EntityFrameworkCore;
using ShopP21.Entities;

namespace ShopP21.Persistence;

public class ShopDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<UserProduct> UsersProducts { get; set; }

    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Налаштування телефону юзера в базі даних
        modelBuilder.Entity<User>().HasIndex(u => u.Phone).IsUnique();
        modelBuilder.Entity<User>()
            .Property(u => u.Phone)
            .IsRequired()
            .HasMaxLength(13);

        // Пошта
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(64);

        // Автоматичний фільтр на невидалені сутності про селектах
        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);

        // Назва продукту
        modelBuilder.Entity<Product>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(64);

        // Автоматичний фільтр на невидалені сутності про селектах
        modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);

        // Двохсторонній зв'язок між юзером і елементом корзини
        modelBuilder.Entity<UserProduct>()
            .HasOne(up => up.User)
            .WithMany(user => user.UserProducts)
            .HasForeignKey(up => up.UserId);

        // Двохсторонній зв'язок між продуктом і елементом корзини
        modelBuilder.Entity<UserProduct>()
            .HasOne(up => up.Product)
            .WithMany(product => product.UsersProduct)
            .HasForeignKey(up => up.ProductId);

        // Композитний primary key
        modelBuilder.Entity<UserProduct>().HasKey(up => new { up.UserId, up.ProductId });
    }
}