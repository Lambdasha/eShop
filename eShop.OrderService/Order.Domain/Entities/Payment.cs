namespace Order.Domain.Entities;

public class Payment
{
    public enum PaymentStatus { Pending, Settled, Failed }
    public int           Id;
    public int           CustomerId;
    public int?          OrderId;           // linked on checkout
    public string        MethodName;
    public decimal       Amount;
    public DateTime      PaidAt;
    public PaymentStatus Status;
}