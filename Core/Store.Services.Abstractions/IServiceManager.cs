using Store.Services.Abstractions.Products;

namespace Store.Services.Abstractions;

public interface IServiceManager
{
    public IProductService ProductService { get; }
}
