namespace Store.Domain.Entities.Baskets;

public class CustomerBasket
{
    public string Id { get; set; }
    public IEnumerable<BasketItem> Items { get; set; }
}
