namespace ProductAPI.Services;
using ProductAPI.DTOs;
public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetProducts();
    Task<ProductDTO> GetProductById(int id);
    Task<ProductDTO> AddProduct(ProductDTO dto);
    Task<ProductDTO> UpdateProduct(int id, ProductDTO dto);
    Task<ProductDTO> RemoveProduct(int id);
}