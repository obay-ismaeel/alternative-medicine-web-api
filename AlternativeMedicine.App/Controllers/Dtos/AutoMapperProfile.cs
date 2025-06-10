using AlternativeMedicine.App.Controllers.Dtos.Incoming;
using AlternativeMedicine.App.Controllers.Dtos.Outgoing;
using AlternativeMedicine.App.Domain.Entities;
using AutoMapper;

namespace AlternativeMedicine.App.Controllers.Dtos;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Category, CategoryDto>()
             .ForMember(dest => dest.Attachment,
                      opt => opt.MapFrom(src => new AttachmentDto { Id = src.Attachment.Id, Path = src.Attachment.Path } ));
        CreateMap<CreateUpdateCategoryDto, Category>();

        CreateMap<Product, ProductDto>();
        CreateMap<CreateUpdateProductDto, Product>();
    }
}
