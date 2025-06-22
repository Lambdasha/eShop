using ProductService.Application.Models;

namespace ProductService.Application.Services;

public interface ICategoryVariationService
{
    Task<CategoryVariationDto>           SaveAsync(CategoryVariationDto dto);
    Task<IEnumerable<CategoryVariationDto>> GetAllAsync();
    Task<CategoryVariationDto?>          GetByIdAsync(int id);
    Task<IEnumerable<CategoryVariationDto>> GetByCategoryIdAsync(int parentId);
    Task<bool>                  DeleteAsync(int id);
}