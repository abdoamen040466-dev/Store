using Store.Domain.Entities.Products;
using Store.Shared.Dtos.Products;

namespace Store.Services.Specifications.Products;

public class ProductsWithBrandAndTypeSpecifications : BaseSpecifications<int, Product>
{
    public ProductsWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
    {
        ApplyIncludes();
    }
    public ProductsWithBrandAndTypeSpecifications(ProductQueryParameters parameters) : base
        (
      p =>
                (!parameters.brandId.HasValue || p.BrandId == parameters.brandId)
                &&
                (!parameters.typeId.HasValue || p.TypeId == parameters.typeId)
                &&
                (string.IsNullOrEmpty(parameters.search) || p.Name.ToLower().Contains(parameters.search.ToLower()))
        )
    {

        ApplyPagination(parameters.pageSize, parameters.pageIndex);

        ApplySorting(parameters.sort);
        ApplyIncludes();
    }

    private void ApplySorting(string? sort)
    {
        if (!string.IsNullOrEmpty(sort))
        {
            switch (sort)
            {
                case "priceasc":
                    AddOrderBy(p => p.Price);
                    break;
                case "pricedesc":
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;

            }
        }
        else
        {
            AddOrderBy(p => p.Name);
        }

    }

    private void ApplyIncludes()
    {
        Includes.Add(p => p.Brand);
        Includes.Add(p => p.Type);

    }
}
