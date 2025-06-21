// ProductService.Application/Services/IProductService.cs
namespace ProductService.Application.Services;

using ProductService.Application.Models;
using System.Threading.Tasks;

public interface IProductService
{
    /// <summary>
    /// Paginated list, optionally filtered by category.
    /// </summary>
    Task<PaginatedResult<ProductDto>> GetProductsAsync(
        int page, int size, int? categoryId = null);

    Task<ProductDto?>       GetByIdAsync(int id);
    Task<ProductDto>        CreateAsync(CreateProductDto dto);
    Task<ProductDto?>       UpdateAsync(int id, UpdateProductDto dto);
    Task<bool>              DeleteAsync(int id);
}