using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Services.Abstractions;
using Store.Shared.Dtos.Orders;
using System.Security.Claims;

namespace Store.Presentation.API;

public class OrdersController(IServiceManager _serviceManager) : APIBaseController
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateOrder(OrderRequest request)
    {
        var userEmailClaim = User.FindFirst(ClaimTypes.Email);
        var result = await _serviceManager.OrderService.CreateOrderAsync(request, userEmailClaim.Value);
        return Ok(result);
    }
    [HttpGet("deliveryMethods")]
    public async Task<IActionResult> GetAllDeliveryMethod()
    {
        var result = await _serviceManager.OrderService.GetAllDeliveryAsync();
        return Ok(result);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetOrdersForSpeceificUser()
    {
        var userEmailClaim = User.FindFirst(ClaimTypes.Email);

        var result = await _serviceManager.OrderService
            .GetOrdersForSpecificUserAsync(userEmailClaim.Value);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetOrderByIdForSpecificUser(Guid id)
    {
        var userEmailClaim = User.FindFirst(ClaimTypes.Email);

        var result = await _serviceManager.OrderService
            .GetOrderByIdForSpecificUserAsync(id, userEmailClaim.Value);
        return Ok(result);
    }
}
