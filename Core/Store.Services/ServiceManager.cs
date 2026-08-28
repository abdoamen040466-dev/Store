using AutoMapper;
using Store.Domain.Contracts;
using Store.Services.Abstractions;
using Store.Services.Abstractions.Products;
using Store.Services.Products;

namespace Store.Services;

public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper) : IServiceManager
{
    public IProductService ProductService { get; } = new ProductService(_unitOfWork, _mapper);
}
