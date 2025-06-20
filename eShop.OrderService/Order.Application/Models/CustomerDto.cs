// Order.Application/Models/CustomerDto.cs
namespace Order.Application.Models;

public class CustomerDto
{
    public int    Id          { get; set; }
    public string FirstName   { get; set; } = default!;
    public string LastName    { get; set; } = default!;
    public string Gender      { get; set; } = default!;
    public string Phone       { get; set; } = default!;
    public string ProfilePic  { get; set; } = default!;
    public string UserId      { get; set; } = default!;
}