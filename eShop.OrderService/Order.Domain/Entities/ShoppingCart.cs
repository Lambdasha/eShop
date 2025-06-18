namespace Order.Domain.Entities;

public class ShoppingCart
{
    public int    Id           { get; set; }
    public int    CustomerId   { get; set; }
    public string CustomerName { get; set; } = default!;

    // 1→N: Cart → Items
    public ICollection<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

}