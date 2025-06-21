// ProductService.Domain/Entities/Product.cs
namespace ProductService.Domain.Entities;

using System.Collections.Generic;

public class Product
{
    public int     Id            { get; set; }
    public string  Name          { get; set; } = default!;
    public string  Description   { get; set; } = default!;
    public int     CategoryId    { get; set; }
    public ProductCategory Category { get; set; } = default!;

    public decimal Price         { get; set; }
    public int     Qty           { get; set; }
    public string  ProductImage  { get; set; } = default!;
    public string  SKU           { get; set; } = default!;

    // N↔N join to variation values
    public ICollection<ProductVariationValue> VariationValues { get; set; }
        = new List<ProductVariationValue>();
}