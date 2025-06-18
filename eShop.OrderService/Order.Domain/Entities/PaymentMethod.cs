namespace Order.Domain.Entities;

public class PaymentMethod
{
    public int     Id             { get; set; }
    public int     PaymentTypeId  { get; set; }
    public string  Provider       { get; set; } = default!;
    public string  AccountNumber  { get; set; } = default!;
    public DateTime Expiry        { get; set; }
    public bool    IsDefault      { get; set; }

    // back-reference to type
    public PaymentType Type      { get; set; } = default!;
}