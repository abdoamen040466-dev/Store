namespace Store.Shared.Dtos.Orders;

public class OrderResponse
{
    public Guid Id { get; set; }
    public string UserEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
    public OrderAddressDto ShippingAddress { get; set; }
    public string DeliveryMethod { get; set; } // DeliveryMethod Name
    public ICollection<OrderItemDto> Items { get; set; }
    public decimal Subtotal { get; set; } // Price * Quantity
    public decimal Total { get; set; } // Subtotal + Delivery Method Cost


}
