namespace Order.Application.Models;

public class UserAddressDto
{
    public int    Id          { get; set; }
    public int    CustomerId  { get; set; }
    public int    AddressId   { get; set; }
    public string Street1     { get; set; } = default!;
    public string? Street2    { get; set; }
    public string City        { get; set; } = default!;
    public string Zipcode     { get; set; } = default!;
    public string State       { get; set; } = default!;
    public string Country     { get; set; } = default!;
    public bool   IsDefault   { get; set; }
}
