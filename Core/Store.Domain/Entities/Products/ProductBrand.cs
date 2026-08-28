namespace Store.Domain.Entities.Products;

public class ProductBrand : BaseEntity<int>
{
    public string Name { get; set; }
    public IEnumerable<Product> Products { get; set; }
}
