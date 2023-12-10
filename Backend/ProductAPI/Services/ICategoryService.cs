using ProductAPI.DTOs;

namespace ProductAPI.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetCategories();
    Task<IEnumerable<CategoryDTO>> GetCategoriesWithProducts();
    Task<CategoryDTO> GetCategoryById(int id);
    Task<CategoryDTO> AddCategory(CategoryDTO dto);
    Task<CategoryDTO> UpdateCategory(int id, CategoryDTO dto);
    Task<CategoryDTO> RemoveCategory(int id);


}