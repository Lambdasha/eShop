namespace Order.Application.Models;

public class CheckoutDto
{
    public string ShippingAddress { get; set; } = default!;
    public string ShippingMethod  { get; set; } = default!;
}