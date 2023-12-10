using ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    public DbSet<Category> Categories { get; set; }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Product>()
            .Property(p => p.Name)
            .HasMaxLength(250)
            .IsRequired();
        
        modelBuilder.Entity<Product>()
            .Property(p => p.ImageUrl)
            .HasMaxLength(250);
        
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(12, 2);
        
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .HasMaxLength(255);

        modelBuilder.Entity<Category>().HasData(new Category[] {
            new() { Id = 1, Name = "Eletrônico" },
            new() { Id = 2, Name = "Limpeza" }
        });

        base.OnModelCreating(modelBuilder);
    }
}