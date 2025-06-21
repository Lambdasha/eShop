// ProductService.Infrastructure/Data/ProductDbContext.cs
namespace ProductService.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public DbSet<ProductCategory>         ProductCategories         { get; set; } = null!;
    public DbSet<CategoryVariation>       CategoryVariations        { get; set; } = null!;
    public DbSet<VariationValue>          VariationValues           { get; set; } = null!;
    public DbSet<Product>                 Products                  { get; set; } = null!;
    public DbSet<ProductVariationValue>   ProductVariationValues    { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);

        // ─── ProductCategory ───────────────────────────────────────────────
        mb.Entity<ProductCategory>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasOne(x => x.ParentCategory)
             .WithMany(x => x.ChildCategories)
             .HasForeignKey(x => x.ParentCategoryId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasMany(x => x.Variations)
             .WithOne(x => x.Category)
             .HasForeignKey(x => x.CategoryId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.Products)
             .WithOne(x => x.Category)
             .HasForeignKey(x => x.CategoryId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // ─── CategoryVariation ────────────────────────────────────────────
        mb.Entity<CategoryVariation>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasOne(x => x.Category)
             .WithMany(x => x.Variations)
             .HasForeignKey(x => x.CategoryId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.Values)
             .WithOne(x => x.Variation)
             .HasForeignKey(x => x.VariationId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // ─── VariationValue ────────────────────────────────────────────────
        mb.Entity<VariationValue>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasOne(x => x.Variation)
             .WithMany(x => x.Values)
             .HasForeignKey(x => x.VariationId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.ProductVariationValues)
             .WithOne(x => x.VariationValue)
             .HasForeignKey(x => x.VariationValueId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // ─── Product ───────────────────────────────────────────────────────
        mb.Entity<Product>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasOne(x => x.Category)
             .WithMany(x => x.Products)
             .HasForeignKey(x => x.CategoryId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.VariationValues)
             .WithOne(x => x.Product)
             .HasForeignKey(x => x.ProductId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // ─── ProductVariationValue (join table) ────────────────────────────
        mb.Entity<ProductVariationValue>(b =>
        {
            b.HasKey(x => new { x.ProductId, x.VariationValueId });

            b.HasOne(x => x.Product)
             .WithMany(x => x.VariationValues)
             .HasForeignKey(x => x.ProductId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.VariationValue)
             .WithMany(x => x.ProductVariationValues)
             .HasForeignKey(x => x.VariationValueId)
             .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
