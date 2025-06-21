// ProductService.Application/Models/UpdateProductDto.cs
namespace ProductService.Application.Models;

public class UpdateProductDto
{
    public string  Name         { get; set; } = default!;
    public string  Description  { get; set; } = default!;
    public int     CategoryId   { get; set; }
    public decimal Price        { get; set; }
    public int     Qty          { get; set; }
    public string  ProductImage { get; set; } = default!;
    public string  SKU          { get; set; } = default!;
}