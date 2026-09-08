using Store.Domain.Entities;

namespace Store.Domain.Entities.Orders;

public class Order : BaseEntity<Guid>
{
    public Order()
    {

    }
    public Order(string userEmail, OrderAddress shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subtotal)
    {
        UserEmail = userEmail;
        ShippingAddress = shippingAddress;
        DeliveryMethod = deliveryMethod;
        Items = items;
        Subtotal = subtotal;
    }

    public string UserEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public OrderAddress ShippingAddress { get; set; }
    public DeliveryMethod DeliveryMethod { get; set; } // navigational
    public int DeliveryMethodId { get; set; } // navigational
    public ICollection<OrderItem> Items { get; set; }
    public decimal Subtotal { get; set; } // Price * Quantity


    //[NotMapped]
    //public decimal Total { get; set; } // Subtotal + Delivery Method Cost

    public decimal GetTotal() => Subtotal + DeliveryMethod.Price; // Not Mapped
}
