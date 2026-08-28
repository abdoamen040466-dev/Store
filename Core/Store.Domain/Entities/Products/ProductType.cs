namespace Store.Domain.Entities.Products;

public class ProductType : BaseEntity<int>
{
    public string Name { get; set; }
    public IEnumerable<Product> Products { get; set; }


}
