using ProductAPI.Context;
using ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Repositories.Impl;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context) 
    {
        _context = context;
    }

    public async Task<Category> Create(Category entity)
    {   
        await _context.Categories.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Category?> Delete(int id)
    {
        var category = await GetById(id);
        
        if (category is not null) 
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        return category;
    }

    public async Task<IEnumerable<Category>> GetAll()
    {
        return await _context.Categories.AsNoTracking().ToListAsync();
    }

    public async Task<Category?> GetById(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        
        if (category is not null) 
        {
            return category;
        }
        
        return null;
    }

    public async Task<IEnumerable<Category>> GetCategoriesWithProducts()
    {
       return await _context.Categories
        .Include(c => c.Products)
        .AsNoTracking()
        .ToListAsync();
    }

    public async Task<Category> Update(Category entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }
}