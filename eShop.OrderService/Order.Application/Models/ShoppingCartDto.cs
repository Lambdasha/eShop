namespace Order.Application.Models;

public class ShoppingCartDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public List<ShoppingCartItemDto> Items { get; set; } = new();
}