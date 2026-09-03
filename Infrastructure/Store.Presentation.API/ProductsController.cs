using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Presentation.API.Attributes;
using Store.Services.Abstractions;
using Store.Shared;
using Store.Shared.Dtos.Products;
using Store.Shared.ErrorModels;

namespace Store.Presentation.API;

public class ProductsController(IServiceManager _serviceManager) : APIBaseController
{
    [HttpGet]
    [Cashe(5 * 60)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResponse<ProductResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<ActionResult<PaginationResponse<ProductResponse>>> GetAllProducts([FromQuery] ProductQueryParameters parameters)
    {
        var result = await _serviceManager.ProductService.GetAllProductAsync(parameters);
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    public async Task<ActionResult<ProductResponse>> GetProductById(int? id)
    {
        var result = await _serviceManager.ProductService.GetProductByIdAsync(id.Value);

        return HandleResult(result);
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
