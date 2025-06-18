namespace Order.Application.Models;

public class CreateOrderDetailDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public int Qty { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
}