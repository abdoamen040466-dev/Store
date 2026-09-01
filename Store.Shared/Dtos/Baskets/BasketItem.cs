namespace Store.Shared.Dtos.Baskets;

public class BasketItemDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int PictureUrl { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

}