using Microsoft.AspNetCore.Mvc;
using Store.Services.Abstractions;
using Store.Shared.Dtos.Baskets;

namespace Store.Presentation.API;

[ApiController]
[Route("api/[controller]")]
public class BasketController(IServiceManager _serviceManager) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBasketById(string id)
    {
        var result = await _serviceManager.BasketService.GetBasketAsync(id);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> CreateOrUpdateBasket(BasketDto dto)
    {
        var result = await _serviceManager.BasketService.CreateBasketAsync(dto, TimeSpan.FromDays(7));
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> DeleteBasket(string id)
    {
        await _serviceManager.BasketService.DeleteBasketAsync(id);
        return NoContent();
    }
}
