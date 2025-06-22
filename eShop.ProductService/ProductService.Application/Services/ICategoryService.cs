using ProductService.Application.Models;

namespace ProductService.Application.Services;

public interface ICategoryService
{
    Task<CategoryDto>           SaveAsync(CategoryDto dto);
    Task<IEnumerable<CategoryDto>> GetAllAsync();
    Task<CategoryDto?>          GetByIdAsync(int id);
    Task<IEnumerable<CategoryDto>> GetByParentIdAsync(int parentId);
    Task<bool>                  DeleteAsync(int id);
}