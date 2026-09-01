using Store.Domain.Entities.Products;
using Store.Shared.Dtos.Products;

namespace Store.Services.Specifications.Products;

internal class ProductCountSpecifications : BaseSpecifications<int, Product>
{
    public ProductCountSpecifications(ProductQueryParameters parameters) : base
        (p =>
                (!parameters.brandId.HasValue || p.BrandId == parameters.brandId)
                &&
                (!parameters.typeId.HasValue || p.TypeId == parameters.typeId)
                &&
                (string.IsNullOrEmpty(parameters.search) || p.Name.ToLower().Contains(parameters.search.ToLower()))

        )
    {

    }
}
