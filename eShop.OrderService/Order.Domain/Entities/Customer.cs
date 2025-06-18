namespace Order.Domain.Entities;

public class Customer
{
    public int    Id               { get; set; }
    public string FirstName        { get; set; } = default!;
    public string LastName         { get; set; } = default!;
    public string Gender           { get; set; } = default!;
    public string Phone            { get; set; } = default!;
    public string ProfilePic       { get; set; } = default!;
    public string UserId           { get; set; } = default!;   // link to your Auth/User service

    // 1→N: Customer → UserAddresses
    public ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();
}