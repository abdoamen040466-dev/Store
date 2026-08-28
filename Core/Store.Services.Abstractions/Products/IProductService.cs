using Store.Shared.Dtos.Products;

namespace Store.Services.Abstractions.Products;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetAllProductAsync();
    Task<ProductResponse> GetProductByIdAsync(int id);
    Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync();
    Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync();
}
