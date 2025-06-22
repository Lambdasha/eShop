// ProductService.Infrastructure/Services/CategoryService.cs

using Microsoft.EntityFrameworkCore;
using ProductService.Application.Models;
using ProductService.Application.Services;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repo;
    public CategoryService(ICategoryRepository repo) 
        => _repo = repo;

    public async Task<CategoryDto> SaveAsync(CategoryDto dto)
    {
        ProductCategory cat;

        if (dto.Id == 0)
        {
            cat = new ProductCategory
            {
                Name             = dto.Name,
                ParentCategoryId = dto.ParentCategoryId
            };
            await _repo.AddAsync(cat);
        }
        else
        {
            cat = await _repo.GetByIdAsync(dto.Id)
                  ?? throw new KeyNotFoundException($"Category {dto.Id} not found");

            cat.Name             = dto.Name;
            cat.ParentCategoryId = dto.ParentCategoryId;
            await _repo.UpdateAsync(cat);
        }

        return new CategoryDto
        {
            Id               = cat.Id,
            Name             = cat.Name,
            ParentCategoryId = cat.ParentCategoryId
        };
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        return list.Select(c => new CategoryDto {
            Id               = c.Id,
            Name             = c.Name,
            ParentCategoryId = c.ParentCategoryId
        });
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var c = await _repo.GetByIdAsync(id);
        if (c == null) return null;
        return new CategoryDto {
            Id               = c.Id,
            Name             = c.Name,
            ParentCategoryId = c.ParentCategoryId
        };
    }

    public async Task<IEnumerable<CategoryDto>> GetByParentIdAsync(int parentId)
    {
        var list = _repo.Query()
                        .Where(c => c.ParentCategoryId == parentId);
        var entities = await list.ToListAsync();
        return entities.Select(c => new CategoryDto {
            Id               = c.Id,
            Name             = c.Name,
            ParentCategoryId = c.ParentCategoryId
        });
    }

    public Task<bool> DeleteAsync(int id)
        => _repo.DeleteAsync(id);
}
