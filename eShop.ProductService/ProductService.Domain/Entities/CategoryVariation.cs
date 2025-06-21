// ProductService.Domain/Entities/CategoryVariation.cs
namespace ProductService.Domain.Entities;

using System.Collections.Generic;

public class CategoryVariation
{
    public int    Id             { get; set; }
    public int    CategoryId     { get; set; }
    public ProductCategory Category { get; set; } = default!;

    public string VariationName { get; set; } = default!;

    // 1→N to allowed values
    public ICollection<VariationValue> Values { get; set; }
        = new List<VariationValue>();
}