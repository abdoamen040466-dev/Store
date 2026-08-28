using AutoMapper;
using Store.Domain.Entities.Products;
using Store.Shared.Dtos.Products;

namespace Store.Services.Mapping.Products;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(pr => pr.Brand, s => s.MapFrom(p => p.Brand.Name))
            .ForMember(pr => pr.Type, s => s.MapFrom(p => p.Type.Name))
            .ReverseMap();

        CreateMap<ProductBrand, BrandTypeResponse>()
            .ReverseMap();

        CreateMap<ProductType, BrandTypeResponse>()
            .ReverseMap();
    }
}
