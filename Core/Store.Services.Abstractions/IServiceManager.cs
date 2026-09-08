using Store.Services.Abstractions.Auth;
using Store.Services.Abstractions.Baskets;
using Store.Services.Abstractions.Cashe;
using Store.Services.Abstractions.Orders;
using Store.Services.Abstractions.Products;

namespace Store.Services.Abstractions;

public interface IServiceManager
{
    public IProductService ProductService { get; }
    public IBasketService BasketService { get; }
    public ICasheService CasheService { get; }
    public IAuthService AuthService { get; }
    public IOrderService OrderService { get; }

}
