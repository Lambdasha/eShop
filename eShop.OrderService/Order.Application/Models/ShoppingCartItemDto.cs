namespace Order.Application.Models;

public class ShoppingCartItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public int Qty { get; set; }
    public decimal Price { get; set; }
}