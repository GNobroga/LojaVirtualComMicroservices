using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using ProductAPI.DTOs;
using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{   
    private readonly ICategoryService service;

    public CategoriesController(ICategoryService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
    {
        var categories = await service.GetCategories();

        return Ok(categories);
    }

    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetWithProducts()
    {
        var categories = await service.GetCategoriesWithProducts();

        return Ok(categories);
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDTO>> GetById(int id)
    {
        var entity = await service.GetCategoryById(id);
        return Ok(entity);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDTO>> Create(CategoryDTO dto)
    {
        var created = await service.AddCategory(dto);

        return new CreatedAtRouteResult("GetCategory", new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, CategoryDTO dto)
    {
        var entity = await service.UpdateCategory(id, dto);

        return Ok(entity);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var entity = await service.RemoveCategory(id);

        return Ok(entity);
    }
}