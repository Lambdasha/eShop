// ProductService.Application/Models/PaginatedResult.cs
namespace ProductService.Application.Models;

using System.Collections.Generic;

public class PaginatedResult<T>
{
    public int              Page       { get; set; }
    public int              Size       { get; set; }
    public int              TotalCount { get; set; }
    public IEnumerable<T>   Items      { get; set; } = default!;
}