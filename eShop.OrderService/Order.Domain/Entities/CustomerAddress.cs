namespace Order.Domain.Entities;

public class CustomerAddress
{
    public int    Id;
    public int    CustomerId;
    public string Line1;
    public string City;
    public string State;
    public string Zip;
    public string Country;
}