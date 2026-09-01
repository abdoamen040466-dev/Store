using Store.Shared;
using Store.Shared.Dtos.Products;

namespace Store.Services.Abstractions.Products;

public interface IProductService
{
    Task<PaginationResponse<ProductResponse>> GetAllProductAsync(ProductQueryParameters parameters);
    Task<ProductResponse> GetProductByIdAsync(int id);
    Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync();
    Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync();
}
