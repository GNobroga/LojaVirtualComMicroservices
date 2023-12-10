using ProductAPI.Context;
using ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Repositories.Impl;

public class ProductRepository : IProductRepository
{   
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product> Create(Product product)
    {   
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();    
        return product;
    }

    public async Task<Product?> Delete(int id)
    {
        var product = await GetById(id);

        if (product is not null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        return product;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _context.Products
            .Include(p => p.Category)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Product?> GetById(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> Update(Product product)
    {
       _context.Entry(product).State = EntityState.Modified;
       await _context.SaveChangesAsync();
       return product;
    }
}