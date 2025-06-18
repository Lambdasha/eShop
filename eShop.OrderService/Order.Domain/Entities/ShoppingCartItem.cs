namespace Order.Domain.Entities;

public class ShoppingCartItem
{
    public int    Id            { get; set; }
    public int    CartId        { get; set; }
    public int    ProductId     { get; set; }
    public string ProductName   { get; set; } = default!;
    public int    Qty           { get; set; }
    public decimal Price        { get; set; }

    // navigation back to parent:
    public ShoppingCart Cart    { get; set; } = default!;
}