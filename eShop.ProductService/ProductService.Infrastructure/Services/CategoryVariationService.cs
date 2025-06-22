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

public class CategoryVariationService : ICategoryVariationService
{
    private readonly ICategoryVariationRepository _repo;
    public CategoryVariationService(ICategoryVariationRepository repo) 
        => _repo = repo;

    public async Task<CategoryVariationDto> SaveAsync(CategoryVariationDto dto)
    {
        CategoryVariation cat;

        if (dto.Id == 0)
        {
            cat = new CategoryVariation
            {
                VariationName = dto.VariationName,
                CategoryId = dto.CategoryId
            };
            await _repo.AddAsync(cat);
        }
        else
        {
            cat = await _repo.GetByIdAsync(dto.Id)
                  ?? throw new KeyNotFoundException($"CategoryVariation {dto.Id} not found");

            cat.VariationName             = dto.VariationName;
            cat.CategoryId = dto.CategoryId;
            await _repo.UpdateAsync(cat);
        }

        return new CategoryVariationDto
        {
            Id = cat.Id,
            VariationName = cat.VariationName,
            CategoryId = cat.CategoryId
        };
    }

    public async Task<IEnumerable<CategoryVariationDto>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        return list.Select(c => new CategoryVariationDto {
            Id               = c.Id,
            VariationName             = c.VariationName,
            CategoryId = c.CategoryId
        });
    }

    public async Task<CategoryVariationDto?> GetByIdAsync(int id)
    {
        var c = await _repo.GetByIdAsync(id);
        if (c == null) return null;
        return new CategoryVariationDto {
            Id               = c.Id,
            VariationName             = c.VariationName,
            CategoryId = c.CategoryId
        };
    }

    public async Task<IEnumerable<CategoryVariationDto>> GetByCategoryIdAsync(int categoryId)
    {
        var list = _repo.Query()
                        .Where(c => c.CategoryId == categoryId);
        var entities = await list.ToListAsync();
        return entities.Select(c => new CategoryVariationDto {
            Id               = c.Id,
            VariationName             = c.VariationName,
            CategoryId = c.CategoryId
        });
    }

    public Task<bool> DeleteAsync(int id)
        => _repo.DeleteAsync(id);
}
