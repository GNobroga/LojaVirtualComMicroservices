using AutoMapper;
using ProductAPI.DTOs;
using ProductAPI.Models;
using ProductAPI.Repositories;

namespace ProductAPI.Services.impl;

public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;

    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDTO> AddCategory(CategoryDTO dto)
    {   
        var entity = _mapper.Map<Category>(dto);
        entity.Id = default;
        var created = await _categoryRepository.Create(entity);
        return _mapper.Map<CategoryDTO>(created);
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategories()
    {
        var categories = await _categoryRepository.GetAll();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategoriesWithProducts()
    {
        var categories = await _categoryRepository.GetCategoriesWithProducts();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
    }

    public async Task<CategoryDTO> GetCategoryById(int id)
    {
        var category = (await _categoryRepository.GetById(id)) ?? 
            throw new Exception($"The category with Id {id} does not exist.");

        return _mapper.Map<CategoryDTO>(category);
    }

    public async Task<CategoryDTO> RemoveCategory(int id)
    {
        _ = (await _categoryRepository.GetById(id)) ?? 
            throw new Exception($"The category with Id {id} does not exist.");
        
        var deleted = await _categoryRepository.Delete(id);
        return _mapper.Map<CategoryDTO>(deleted);
    }

    public async Task<CategoryDTO> UpdateCategory(int id, CategoryDTO dto)
    {
        var category = (await _categoryRepository.GetById(id)) ?? 
            throw new Exception($"The category with Id {id} does not exist.");

        var updateCategory = _mapper.Map(dto, category);

        updateCategory.Id = id;
        
        var updated = await _categoryRepository.Update(updateCategory);

        return _mapper.Map<CategoryDTO>(updated);
    }
}