namespace Order.Domain.Entities;

public class PaymentType
{
    public int    Id      { get; set; }
    public string Name    { get; set; } = default!;

    // 1→N: Type → Methods
    public ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();
}