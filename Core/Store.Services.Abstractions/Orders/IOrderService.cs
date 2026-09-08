using Store.Shared.Dtos.Orders;

namespace Store.Services.Abstractions.Orders;

public interface IOrderService
{
    Task<OrderResponse?> CreateOrderAsync(OrderRequest request, string userEmail);
    Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryAsync();
    Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid id, string userEmail);
    Task<IEnumerable<OrderResponse?>> GetOrdersForSpecificUserAsync(string userEmail);
}
