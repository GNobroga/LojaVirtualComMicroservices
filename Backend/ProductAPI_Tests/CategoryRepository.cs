using ProductAPI.Context;
using ProductAPI.Models;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Repositories;

namespace ProductAPI_Tests;

public class CategoryRepository : ICategoryRepository
{

    public readonly List<Category> _categories = new() {
        new() { Id = 1, Name = "ELETRONICOS" }
    };

    public async Task<Category> Create(Category entity)
    {      
        _categories.Add(entity);
        return entity;
    }

    public async Task<Category?> Delete(int id)
    {
        var category = await GetById(id);
        
        if (category is not null) 
        {
            _categories.Remove(category);
        }

        return category;
    }

    public Task<IEnumerable<Category>> GetAll()
    {
        return Task.Run<IEnumerable<Category>>(() => _categories);
    }

    public async Task<Category?> GetById(int id)
    {
        var category = _categories.FirstOrDefault(c => c.Id == id);
        
        if (category is not null) 
        {
            return category;
        }
        
        return null;
    }

    public async Task<IEnumerable<Category>> GetCategoriesWithProducts()
    {
       return _categories;
    }

    public async Task<Category> Update(Category entity)
    {
        var category = _categories.FirstOrDefault(c => c.Id == entity.Id);
        category.Name = entity.Name;
        category.Products = entity.Products;
        return entity;
    }
}