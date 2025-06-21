// ProductService.Domain/Entities/ProductCategory.cs
namespace ProductService.Domain.Entities;

using System.Collections.Generic;

public class ProductCategory
{
    public int    Id               { get; set; }
    public string Name             { get; set; } = default!;

    // self-referencing parent/child
    public int?                   ParentCategoryId { get; set; }
    public ProductCategory?       ParentCategory   { get; set; }
    public ICollection<ProductCategory> ChildCategories { get; set; }
        = new List<ProductCategory>();

    // 1→N to variations
    public ICollection<CategoryVariation> Variations { get; set; }
        = new List<CategoryVariation>();

    // 1→N to products
    public ICollection<Product> Products { get; set; }
        = new List<Product>();
}