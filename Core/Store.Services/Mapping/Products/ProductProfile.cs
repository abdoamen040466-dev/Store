using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Domain.Entities.Products;
using Store.Shared.Dtos.Products;

namespace Store.Services.Mapping.Products;

public class ProductProfile : Profile
{
    public ProductProfile(IConfiguration configuration)
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(pr => pr.Brand, s => s.MapFrom(p => p.Brand.Name))
            .ForMember(pr => pr.Type, s => s.MapFrom(p => p.Type.Name))
            //.ForMember(pr => pr.PictureUrl, s => s.MapFrom(p => $"{configuration["BaseUrl"]}/{p.PictureUrl}"))
            .ForMember(pr => pr.PictureUrl, s => s.MapFrom(new ProductPictureUrlResolver(configuration)))
            .ReverseMap();

        CreateMap<ProductBrand, BrandTypeResponse>()
            .ReverseMap();

        CreateMap<ProductType, BrandTypeResponse>()
            .ReverseMap();

    }
}
