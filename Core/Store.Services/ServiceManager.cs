using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Store.Domain.Contracts;
using Store.Domain.Entities.Identity;
using Store.Services.Abstractions;
using Store.Services.Abstractions.Auth;
using Store.Services.Abstractions.Baskets;
using Store.Services.Abstractions.Cashe;
using Store.Services.Abstractions.Orders;
using Store.Services.Abstractions.Products;
using Store.Services.Auth;
using Store.Services.Baskets;
using Store.Services.Cashe;
using Store.Services.Orders;
using Store.Services.Products;
using Store.Shared;

namespace Store.Services;

public class ServiceManager
    (IUnitOfWork _unitOfWork,
    IBasketRepository _basketRepository,
    ICasheRepository _casheRepository,
    UserManager<AppUser> _userManager,
    IOptions<JwtOptions> options,
    IMapper _mapper)
    : IServiceManager
{
    public IProductService ProductService { get; } = new ProductService(_unitOfWork, _mapper);
    public IBasketService BasketService { get; } = new BasketService(_basketRepository, _mapper);
    public ICasheService CasheService { get; } = new CasheService(_casheRepository);
    public IAuthService AuthService { get; } = new AuthService(_userManager, options);

    public IOrderService OrderService { get; } = new OrderService(_unitOfWork, _mapper, _basketRepository);

}
