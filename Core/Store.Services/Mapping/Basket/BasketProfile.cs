using AutoMapper;
using Store.Domain.Entities.Baskets;
using Store.Shared.Dtos.Baskets;

namespace Store.Services.Mapping.Basket;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<CustomerBasket, BasketDto>()
            .ReverseMap();
        CreateMap<BasketItem, BasketItemDto>()
            .ReverseMap();
    }
}
