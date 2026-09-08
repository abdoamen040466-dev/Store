using AutoMapper;
using Store.Domain.Contracts;
using Store.Domain.Entities.Orders;
using Store.Domain.Entities.Products;
using Store.Domain.Exceptions.BadRequest;
using Store.Domain.Exceptions.NotFound;
using Store.Services.Abstractions.Orders;
using Store.Services.Specifications.Orders;
using Store.Shared.Dtos.Orders;

namespace Store.Services.Orders;

public class OrderService(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository _basketRepository) : IOrderService
{
    //private readonly _orderRepository  = _unitOfWork.GetRepository<Guid, Order>();
    public async Task<OrderResponse?> CreateOrderAsync(OrderRequest request, string userEmail)
    {

        // 1. get orderAddress
        var orderAddress = _mapper.Map<OrderAddress>(request.ShipedToAddress);

        // 2. get Delivery method by id
        var deliveryMethod = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAsync(request.DeliveryMethodId);
        if (deliveryMethod is null) throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);

        // 3. get order items
        // 3.1 get basket by id
        var basket = await _basketRepository.GetBasketAsync(request.BasketId);
        if (basket is null) throw new BasketNotFoundException(request.BasketId);
        // 3.2 convert every basket item to order item
        var orderItems = new List<OrderItem>();
        foreach (var item in basket.Items)
        {
            // check price
            var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(item.Id);
            if (product is null) throw new ProductNotFoundException(item.Id);
            if (product.Price != item.Price)
                item.Price = product.Price;

            var productInOrderItem = new ProductInOrderItem(item.Id, item.ProductName, item.PictureUrl);
            var orderItem = new OrderItem(productInOrderItem, item.Price, item.Quantity);

            orderItems.Add(orderItem);
        }

        // 4. calculate subtotal
        var subtotal = orderItems.Sum(item => item.Price * item.Quantity);




        // Create Order

        var order = new Order(userEmail, orderAddress, deliveryMethod, orderItems, subtotal);

        // Create Order In Database
        await _unitOfWork.GetRepository<Guid, Order>().AddAsync(order);
        var count = await _unitOfWork.SaveChangesAsync();

        if (count <= 0) throw new CreateOrderBadRequest();

        return _mapper.Map<OrderResponse>(order);
    }

    public async Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryAsync()
    {
        var deliveryMethods = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAllAsync();
        return _mapper.Map<IEnumerable<DeliveryMethodResponse>>(deliveryMethods);
    }

    public async Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid id, string userEmail)
    {
        var spec = new OrderSpecification(id, userEmail);
        var order = await _unitOfWork.GetRepository<Guid, Order>().GetAsync(spec);
        if (order is null) throw new OrderNotFoundException(id);

        return _mapper.Map<OrderResponse>(order);
    }

    public async Task<IEnumerable<OrderResponse?>> GetOrdersForSpecificUserAsync(string userEmail)
    {
        var spec = new OrderSpecification(userEmail);
        var order = await _unitOfWork.GetRepository<Guid, Order>().GetAllAsync(spec);

        return _mapper.Map<IEnumerable<OrderResponse>>(order);
    }
}
