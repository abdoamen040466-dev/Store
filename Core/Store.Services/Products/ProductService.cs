using AutoMapper;
using Store.Domain.Contracts;
using Store.Domain.Entities.Products;
using Store.Services.Abstractions.Products;
using Store.Shared.Dtos.Products;

namespace Store.Services.Products;

public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
{
    public async Task<IEnumerable<ProductResponse>> GetAllProductAsync()
    {
        var product = await _unitOfWork.GetRepository<int, Product>().GetAllAsync();
        return _mapper.Map<IEnumerable<ProductResponse>>(product);
    }
    public async Task<ProductResponse> GetProductByIdAsync(int id)
    {
        var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(id);
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
