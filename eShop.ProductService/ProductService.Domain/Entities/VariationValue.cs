// ProductService.Domain/Entities/VariationValue.cs
namespace ProductService.Domain.Entities;

using System.Collections.Generic;

public class VariationValue
{
    public int    Id            { get; set; }
    public int    VariationId   { get; set; }
    public CategoryVariation Variation    { get; set; } = default!;

    public string Value         { get; set; } = default!;

    // N↔N join to products
    public ICollection<ProductVariationValue> ProductVariationValues { get; set; }
        = new List<ProductVariationValue>();
}