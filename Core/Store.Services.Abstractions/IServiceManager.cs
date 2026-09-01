using Store.Services.Abstractions.Baskets;
using Store.Services.Abstractions.Products;

namespace Store.Services.Abstractions;

public interface IServiceManager
{
    public IProductService ProductService { get; }
    public IBasketService BasketService { get; }
}
