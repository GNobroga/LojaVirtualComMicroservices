using ProductAPI.Models;

namespace ProductAPI.Repositories;

public interface IProductRepository 
{
    Task<IEnumerable<Product>> GetAll();

    Task<Product?> GetById(int id);

    Task<Product> Create(Product product);

    Task<Product> Update(Product product);

    Task<Product?> Delete(int id);
}