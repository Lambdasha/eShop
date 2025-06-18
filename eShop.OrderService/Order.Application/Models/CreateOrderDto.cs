namespace Order.Application.Models;

public class CreateOrderDto
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = default!;
    public string ShippingAddress { get; set; } = default!;
    public string ShippingMethod { get; set; } = default!;
    public List<CreateOrderDetailDto> Details { get; set; } = new();
}