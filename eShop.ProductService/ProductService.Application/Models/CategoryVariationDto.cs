namespace ProductService.Application.Models;

public class CategoryVariationDto
{
    public int    Id               { get; set; }
    public string VariationName             { get; set; } = default!;
    public int   CategoryId { get; set; }
}