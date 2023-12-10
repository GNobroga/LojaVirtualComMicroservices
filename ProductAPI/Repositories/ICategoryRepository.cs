using ProductAPI.Models;

namespace ProductAPI.Repositories;

public interface ICategoryRepository 
{
    Task<IEnumerable<Category>> GetAll();
    Task<IEnumerable<Category>> GetCategoriesWithProducts();

    Task<Category?> GetById(int id);

    Task<Category> Create(Category entity);

    Task<Category> Update(Category entity);

    Task<Category?> Delete(int id);
}