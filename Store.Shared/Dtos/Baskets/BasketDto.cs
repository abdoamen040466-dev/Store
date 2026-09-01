namespace Store.Shared.Dtos.Baskets;

public class BasketDto
{
    public string Id { get; set; }
    public IEnumerable<BasketItemDto> items { get; set; }
}
