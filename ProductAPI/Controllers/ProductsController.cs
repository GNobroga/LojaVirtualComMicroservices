using Microsoft.AspNetCore.Cors;
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
public class ProductsController : ControllerBase
{   
    private readonly IProductService service;

    public ProductsController(IProductService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
    {
        var products = await service.GetProducts();

        return Ok(products);
    }

    [HttpGet("{id:int}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDTO>> GetById(int id)
    {
        var entity = await service.GetProductById(id);
        return Ok(entity);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> Create(ProductDTO dto)
    {
        var created = await service.AddProduct(dto);

        return new CreatedAtRouteResult("GetProduct", new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, ProductDTO dto)
    {
        var entity = await service.UpdateProduct(id, dto);

        return Ok(entity);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var entity = await service.RemoveProduct(id);

        return Ok(entity);
    }
}