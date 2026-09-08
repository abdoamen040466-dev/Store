using AutoMapper;
using Store.Domain.Contracts;
using Store.Domain.Entities.Baskets;
using Store.Services.Abstractions.Baskets;
using Store.Services.Abstractions.Common;
using Store.Shared.Dtos.Baskets;

namespace Store.Services.Baskets;

public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
{
    public async Task<Result<BasketDto?>> GetBasketAsync(string id)
    {
        var basket = await _basketRepository.GetBasketAsync(id);
        if (basket is null) return Error.NotFound(description: $"Basket with id : {id} is not found");
        return _mapper.Map<BasketDto>(basket);
    }
    public async Task<Result<BasketDto?>> CreateBasketAsync(BasketDto basketdto, TimeSpan duration)
    {
        var basket = _mapper.Map<CustomerBasket>(basketdto);
        var result = await _basketRepository.CreateBasketAsync(basket, duration);
        if (result is null) return Error.Failure();

        return _mapper.Map<BasketDto>(result);
    }

    public async Task<Result> DeleteBasketAsync(string id)
    {
        var flag = await _basketRepository.DeleteBasketAsync(id);
        if (!flag) return Result.Fail(Error.NotFound(description: $"Basket with id : {id} is not found"));
        return Result.Ok();
    }

}
