using Store.Services.Abstractions.Common;
using Store.Shared;
using Store.Shared.Dtos.Products;

namespace Store.Services.Abstractions.Products;

public interface IProductService
{
    Task<Result<PaginationResponse<ProductResponse>>> GetAllProductAsync(ProductQueryParameters parameters);
    Task<Result<ProductResponse>> GetProductByIdAsync(int id);
    Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync();
    Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync();
}
