// ProductService.Domain/Entities/ProductVariationValue.cs
namespace ProductService.Domain.Entities;

public class ProductVariationValue
{
    // composite key ProductId+VariationValueId, will configure in DbContext
    public int ProductId            { get; set; }
    public Product Product          { get; set; } = default!;

    public int VariationValueId     { get; set; }
    public VariationValue VariationValue { get; set; } = default!;
}