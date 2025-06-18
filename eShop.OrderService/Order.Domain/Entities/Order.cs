namespace Order.Domain.Entities;
public class Order
{
    public enum OrderStatus { Pending, Completed, Cancelled }
    
    public int                 Id               { get; set; }
    public DateTime            OrderDate        { get; set; }
    public int                 CustomerId       { get; set; }
    public string              CustomerName     { get; set; } = default!;
    public int?                PaymentMethodId  { get; set; }
    public string?             ShippingAddress  { get; set; }
    public string?             ShippingMethod   { get; set; }
    public decimal             BillAmount       { get; set; }
    public OrderStatus         Status           { get; set; } = OrderStatus.Pending;

    // 1→N: Order → OrderDetails
    public ICollection<OrderDetail> Details     { get; set; } = new List<OrderDetail>();
}