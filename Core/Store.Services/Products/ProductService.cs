using AutoMapper;
using Store.Domain.Contracts;
using Store.Domain.Entities.Products;
using Store.Services.Abstractions.Common;
using Store.Services.Abstractions.Products;
using Store.Services.Specifications.Products;
using Store.Shared;
using Store.Shared.Dtos.Products;

namespace Store.Services.Products;

public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
{
    public async Task<Result<PaginationResponse<ProductResponse>>> GetAllProductAsync(ProductQueryParameters parameters)
    {
        var spec = new ProductsWithBrandAndTypeSpecifications(parameters);

        var product = await _unitOfWork.GetRepository<int, Product>().GetAllAsync(spec);
        var productResponse = _mapper.Map<IEnumerable<ProductResponse>>(product);


        var countSpec = new ProductCountSpecifications(parameters);
        var count = await _unitOfWork.GetRepository<int, Product>().CountAsync(countSpec);
        return Result<PaginationResponse<ProductResponse>>.Ok(new PaginationResponse<ProductResponse>(parameters.pageIndex, parameters.pageSize, count, productResponse));
    }

    public async Task<Result<ProductResponse>> GetProductByIdAsync(int id)
    {
        var spec = new ProductsWithBrandAndTypeSpecifications(id);

        var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(spec, id);

        if (product is null)
            return Error.NotFound();

        return _mapper.Map<ProductResponse>(product);

    }
    public async Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync()
    {
        var brands = await _unitOfWork.GetRepository<int, ProductBrand>().GetAllAsync();
        return _mapper.Map<IEnumerable<BrandTypeResponse>>(brands);
    }
    public async Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync()
    {
        var types = await _unitOfWork.GetRepository<int, ProductType>().GetAllAsync();
        return _mapper.Map<IEnumerable<BrandTypeResponse>>(types);
    }

}
