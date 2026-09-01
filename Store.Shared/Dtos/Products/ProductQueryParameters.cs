namespace Store.Shared.Dtos.Products;

public class ProductQueryParameters
{
    public int? brandId { get; set; }
    public int? typeId { get; set; }
    public string? sort { get; set; }
    public string? search { get; set; }
    public int pageIndex { get; set; } = 1;
    public int pageSize { get; set; } = 5;
}
