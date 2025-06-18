namespace Order.Application.Models;

public class PaginatedResult<T>
{
    public int Page { get; set; }
    public int Size { get; set; }
    public long TotalCount { get; set; }
    public List<T> Items { get; set; } = new();
}