using Store.Domain.Entities.Baskets;

namespace Store.Domain.Contracts;

public interface IBasketRepository
{
    Task<CustomerBasket?> GetBasketAsync(string Id);
    Task<CustomerBasket?> CreateBasketAsync(CustomerBasket basket, TimeSpan duration);
    Task<bool> DeleteBasketAsync(string id);
}
