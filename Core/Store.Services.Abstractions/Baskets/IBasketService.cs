using Store.Services.Abstractions.Common;
using Store.Shared.Dtos.Baskets;

namespace Store.Services.Abstractions.Baskets;

public interface IBasketService
{
    Task<Result<BasketDto?>> GetBasketAsync(string id);
    Task<Result<BasketDto?>> CreateBasketAsync(BasketDto basketdto, TimeSpan duration);
    Task<Result> DeleteBasketAsync(string id);
}
