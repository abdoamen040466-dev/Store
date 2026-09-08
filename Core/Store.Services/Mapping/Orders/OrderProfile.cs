using AutoMapper;
using Store.Domain.Entities.Orders;
using Store.Shared.Dtos.Orders;

namespace Store.Services.Mapping.Orders;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderAddressDto, OrderAddress>().ReverseMap();

        CreateMap<Order, OrderResponse>()
            .ForMember(d => d.DeliveryMethod, opt => opt.MapFrom(s => s.DeliveryMethod.ShortName))
            .ForMember(d => d.Total, opt => opt.MapFrom(s => s.GetTotal()))
            ;

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.Product.ProductId))
            .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product.ProductName))
            .ForMember(d => d.PictureUrl, opt => opt.MapFrom(s => s.Product.PictureUrl))
            ;

        CreateMap<DeliveryMethod, DeliveryMethodResponse>().ReverseMap();

    }
}
