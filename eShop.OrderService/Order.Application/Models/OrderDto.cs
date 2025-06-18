namespace Order.Application.Models;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = default!;
    public string ShippingAddress { get; set; } = default!;
    public string ShippingMethod { get; set; } = default!;
    public decimal BillAmount { get; set; }
    public string Status { get; set; } = default!;
    public List<OrderDetailDto> Details { get; set; } = new();
}