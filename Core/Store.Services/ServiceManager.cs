using AutoMapper;
using Store.Domain.Contracts;
using Store.Services.Abstractions;
using Store.Services.Abstractions.Baskets;
using Store.Services.Abstractions.Cashe;
using Store.Services.Abstractions.Products;
using Store.Services.Baskets;
using Store.Services.Cashe;
using Store.Services.Products;

namespace Store.Services;

public class ServiceManager
    (IUnitOfWork _unitOfWork,
    IBasketRepository _basketRepository,
    ICasheRepository _casheRepository,
    IMapper _mapper)
    : IServiceManager
{
    public IProductService ProductService { get; } = new ProductService(_unitOfWork, _mapper);
    public IBasketService BasketService { get; } = new BasketService(_basketRepository, _mapper);
    public ICasheService CasheService { get; } = new CasheService(_casheRepository);
}
