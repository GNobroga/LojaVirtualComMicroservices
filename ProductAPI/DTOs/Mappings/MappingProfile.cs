using AutoMapper;
using ProductAPI.Models;

namespace ProductAPI.DTOs.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<ProductDTO, Product>();
        CreateMap<Product, ProductDTO>()
            .ForMember(dest => dest.CategoriaName, opt => opt.MapFrom(src => src.Category!.Name));
    }
}