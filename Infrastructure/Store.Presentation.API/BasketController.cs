using Microsoft.AspNetCore.Mvc;
using Store.Services.Abstractions;
using Store.Shared.Dtos.Baskets;

namespace Store.Presentation.API;

public class BasketsController(IServiceManager _serviceManager) : APIBaseController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<BasketDto>> GetBasketById(string id)
    {
        var result = await _serviceManager.BasketService.GetBasketAsync(id);
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto dto)
    {
        var result = await _serviceManager.BasketService.CreateBasketAsync(dto, TimeSpan.FromDays(7));
        return HandleResult(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBasket(string id)
    {
        var result = await _serviceManager.BasketService.DeleteBasketAsync(id);
        return HandleResult(result);
    }
}
