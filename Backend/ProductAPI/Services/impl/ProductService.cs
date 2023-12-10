namespace ProductAPI.Services.impl;

using AutoMapper;
using ProductAPI.DTOs;
using ProductAPI.Models;
using ProductAPI.Repositories;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;

    private readonly IProductRepository _productRepository;

    private readonly ICategoryService _categoryService;

    public ProductService(IMapper mapper, IProductRepository productRepository, ICategoryService categoryService)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _categoryService = categoryService;
    }

    public async Task<ProductDTO> AddProduct(ProductDTO dto)
    {   
        await _categoryService.GetCategoryById(dto.CategoryId);
    
        var entity = _mapper.Map<Product>(dto);
        entity.Id = default;
        var created = await _productRepository.Create(entity);
        return _mapper.Map<ProductDTO>(created);
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var products = await _productRepository.GetAll();
        return _mapper.Map<IEnumerable<ProductDTO>>(products);
    }

    public async Task<ProductDTO> GetProductById(int id)
    {
        var product = (await _productRepository.GetById(id)) ?? 
            throw new Exception($"The product with Id {id} does not exist.");

        return _mapper.Map<ProductDTO>(product);
    }

    public async Task<ProductDTO> RemoveProduct(int id)
    {
        _ = (await _productRepository.GetById(id)) ?? 
            throw new Exception($"The product with Id {id} does not exist.");
        
        var deleted = await _productRepository.Delete(id);
        return _mapper.Map<ProductDTO>(deleted);
    }

    public async Task<ProductDTO> UpdateProduct(int id, ProductDTO dto)
    {
        var product = (await _productRepository.GetById(id)) ?? 
            throw new Exception($"The product with Id {id} does not exist.");

        await _categoryService.GetCategoryById(dto.CategoryId);

        var updateProduct = _mapper.Map(dto, product);

        updateProduct.Id = id;
        
        var updated = await _productRepository.Update(updateProduct);

        return _mapper.Map<ProductDTO>(updated);
    }
}