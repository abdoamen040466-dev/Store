using Store.Shared.Dtos.Baskets;

namespace Store.Services.Abstractions.Baskets;

public interface IBasketService
{
    Task<BasketDto?> GetBasketAsync(string id);
    Task<BasketDto?> CreateBasketAsync(BasketDto basketdto, TimeSpan duration);
    Task<bool> DeleteBasketAsync(string id);
}
