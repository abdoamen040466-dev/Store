using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Services.Abstractions;
using Store.Shared;
using Store.Shared.Dtos.Products;
using Store.Shared.ErrorModels;

namespace Store.Presentation.API;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IServiceManager _serviceManager) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResponse<ProductResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<ActionResult<PaginationResponse<ProductResponse>>> GetAllProducts([FromQuery] ProductQueryParameters parameters)
    {
        var result = await _serviceManager.ProductService.GetAllProductAsync(parameters);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    public async Task<ActionResult<ProductResponse>> GetProductById(int? id)
    {
        if (id is null) return BadRequest();
        var result = await _serviceManager.ProductService.GetProductByIdAsync(id.Value);



        return Ok(result);
    }

    [HttpGet("brands")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandTypeResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<ActionResult<IEnumerable<BrandTypeResponse>>> GetAllBrands()
    {
        var result = await _serviceManager.ProductService.GetAllBrandsAsync();
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpGet("types")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandTypeResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<ActionResult<IEnumerable<BrandTypeResponse>>> GetAllTypes()
    {
        var result = await _serviceManager.ProductService.GetAllTypesAsync();
        if (result is null) return NotFound();
        return Ok(result);
    }
}
