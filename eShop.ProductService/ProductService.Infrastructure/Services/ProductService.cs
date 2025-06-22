// ProductService.Infrastructure/Services/ProductService.cs

using ProductService.Application.Models;
using ProductService.Application.Services;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Services;
public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    public ProductService(IProductRepository repo) 
        => _repo = repo;

    public async Task<PaginatedResult<ProductDto>> GetProductsAsync(
        int page,
        int size,
        int? categoryId = null)
    {
        // 1) start from the base query
        var query = _repo.Query();

        // 2) apply category filter if provided
        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId.Value);

        // 3) get total count before paging
        var total = await query.CountAsync();

        // 4) apply paging
        var products = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();

        // 5) project to DTOs
        var items = products.Select(p => new ProductDto
        {
            Id           = p.Id,
            Name         = p.Name,
            Description  = p.Description,
            CategoryId   = p.CategoryId,
            Price        = p.Price,
            Qty          = p.Qty,
            ProductImage = p.ProductImage,
            SKU          = p.SKU
        });

        // 6) return paginated result
        return new PaginatedResult<ProductDto>
        {
            Page       = page,
            Size       = size,
            TotalCount = total,
            Items      = items
        };
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var p = await _repo.GetByIdAsync(id);

        if (p == null) return null;

        return new ProductDto {
            Id           = p.Id,
            Name         = p.Name,
            Description  = p.Description,
            CategoryId   = p.CategoryId,
            Price        = p.Price,
            Qty          = p.Qty,
            ProductImage = p.ProductImage,
            SKU          = p.SKU
        };
    }
    
    public async Task<IEnumerable<ProductDto>> GetByNameAsync(string name)
    {
        var products = await _repo
            .Query()
            .Where(p => p.Name.Contains(name))
            .ToListAsync();
        // still materialize here

        return products.Select(p => new ProductDto {
            Id           = p.Id,
            Name         = p.Name,
            Description  = p.Description,
            CategoryId   = p.CategoryId,
            Price        = p.Price,
            Qty          = p.Qty,
            ProductImage = p.ProductImage,
            SKU          = p.SKU
        });
    }


    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var p = new Product {
            Name         = dto.Name,
            Description  = dto.Description,
            CategoryId   = dto.CategoryId,
            Price        = dto.Price,
            Qty          = dto.Qty,
            ProductImage = dto.ProductImage,
            SKU          = dto.SKU
        };

        await _repo.AddAsync(p);

        return new ProductDto {
            Id           = p.Id,
            Name         = p.Name,
            Description  = p.Description,
            CategoryId   = p.CategoryId,
            Price        = p.Price,
            Qty          = p.Qty,
            ProductImage = p.ProductImage,
            SKU          = p.SKU
        };
    }

    public async Task<ProductDto?> UpdateAsync(int id, UpdateProductDto dto)
    {
        var p = await _repo.GetByIdAsync(id);
        if (p == null) return null;

        p.Name         = dto.Name;
        p.Description  = dto.Description;
        p.CategoryId   = dto.CategoryId;
        p.Price        = dto.Price;
        p.Qty          = dto.Qty;
        p.ProductImage = dto.ProductImage;
        p.SKU          = dto.SKU;

        await _repo.UpdateAsync(p);

        return new ProductDto {
            Id           = p.Id,
            Name         = p.Name,
            Description  = p.Description,
            CategoryId   = p.CategoryId,
            Price        = p.Price,
            Qty          = p.Qty,
            ProductImage = p.ProductImage,
            SKU          = p.SKU
        };
    }

    public Task<bool> DeleteAsync(int id)
        => _repo.DeleteAsync(id);
    
    public Task<bool> InactivateAsync(int id)
    {
        // optional business checks here
        return _repo.InactivateAsync(id);
    }
}
