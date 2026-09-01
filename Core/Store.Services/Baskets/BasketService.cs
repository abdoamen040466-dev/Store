using AutoMapper;
using Store.Domain.Contracts;
using Store.Domain.Entities.Baskets;
using Store.Domain.Exceptions.BadRequest;
using Store.Domain.Exceptions.NotFound;
using Store.Services.Abstractions.Baskets;
using Store.Shared.Dtos.Baskets;

namespace Store.Services.Baskets;

public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
{
    public async Task<BasketDto?> GetBasketAsync(string id)
    {
        var basket = await _basketRepository.GetBasketAsync(id);
        if (basket is null) throw new BasketNotFoundException(id);
        return _mapper.Map<BasketDto>(basket);
    }
    public async Task<BasketDto?> CreateBasketAsync(BasketDto basketdto, TimeSpan duration)
    {
        var basket = _mapper.Map<CustomerBasket>(basketdto);
        var result = await _basketRepository.CreateBasketAsync(basket, duration);
        if (result is null) throw new DeleteBasketBadRequestException();

        return _mapper.Map<BasketDto>(result);
    }

    public async Task<bool> DeleteBasketAsync(string id)
    {
        var flag = await _basketRepository.DeleteBasketAsync(id);
        if (!flag) throw new DeleteBasketBadRequestException();
        return flag;
    }

}
