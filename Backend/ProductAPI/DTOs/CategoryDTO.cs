using System.ComponentModel.DataAnnotations;

namespace ProductAPI.DTOs;

public class CategoryDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The {0} is required.")]
    public string? Name { get; set; }

    public ICollection<ProductDTO>? Products { get; set; }
}