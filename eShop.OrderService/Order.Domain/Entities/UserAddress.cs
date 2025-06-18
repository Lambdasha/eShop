namespace Order.Domain.Entities;

public class UserAddress
{
    public int  Id                 { get; set; }
    public int  CustomerId         { get; set; }
    public int  AddressId          { get; set; }
    public bool IsDefaultAddress   { get; set; }

    // navigation props for the two FKs:
    public Customer Customer       { get; set; } = default!;
    public Address  Address        { get; set; } = default!;
}