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
        int page, int size, int? categoryId = null)
    {
        // start from IQueryable<Product>
        var query = _repo.Query();

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId);

        var total = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();

        var dtos = items.Select(p => new ProductDto {
            Id           = p.Id,
            Name         = p.Name,
            Description  = p.Description,
            CategoryId   = p.CategoryId,
            Price        = p.Price,
            Qty          = p.Qty,
            ProductImage = p.ProductImage,
            SKU          = p.SKU
        });

        return new PaginatedResult<ProductDto> {
            Page       = page,
            Size       = size,
            TotalCount = total,
            Items      = dtos
        };
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var p = await _repo.Query()
            .FirstOrDefaultAsync(x => x.Id == id);

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
}
