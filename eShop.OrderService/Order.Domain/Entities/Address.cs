namespace Order.Domain.Entities;

public class Address
{
    public int    Id       { get; set; }
    public string Street1  { get; set; } = default!;
    public string? Street2 { get; set; }
    public string City     { get; set; } = default!;
    public string Zipcode  { get; set; } = default!;
    public string State    { get; set; } = default!;
    public string Country  { get; set; } = default!;

    // 1→N: Address → UserAddresses
    public ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();
}