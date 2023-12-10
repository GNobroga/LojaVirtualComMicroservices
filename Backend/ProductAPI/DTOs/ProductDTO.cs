using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProductAPI.Models;

namespace ProductAPI.DTOs;
public class ProductDTO
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "The {0} is required")]
    public string? Name { get; set; }

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public long Stock { get; set; }

    public string? ImageUrl { get; set; }

    public string? CategoriaName { get; set; }

    [Required(ErrorMessage = "The {0} is required")]
    public int CategoryId { get; set; }
}