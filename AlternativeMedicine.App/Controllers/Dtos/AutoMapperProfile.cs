using AlternativeMedicine.App.Controllers.Dtos.Incoming;
using AlternativeMedicine.App.Controllers.Dtos.Outgoing;
using AlternativeMedicine.App.Domain.Entities;
using AutoMapper;

namespace AlternativeMedicine.App.Controllers.Dtos;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Category, CategoryDto>();

        CreateMap<CreateUpdateCategoryDto, Category>();

        CreateMap<Product, ProductDto>();

        CreateMap<CreateUpdateProductDto, Product>();
    }
}
