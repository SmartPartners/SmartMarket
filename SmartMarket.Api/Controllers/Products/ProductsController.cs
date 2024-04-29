using Microsoft.AspNetCore.Mvc;
using SmartMarket.Api.Controllers.Commons;
using SmartMarket.Domin.Configurations;
using SmartMarket.Service.DTOs.Products;
using SmartMarket.Service.Interfaces.Products;
using System.ComponentModel.DataAnnotations;

namespace SmartMarket.Api.Controllers.Products;

public class ProductsController : BaseController
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromForm] ProductForCreationDto productForCreationDto)
        => Ok(await _productService.CreateAsync(productForCreationDto));

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(await _productService.GetAllAsync(@params));


    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([Required] long id)
        => Ok(await _productService.GetByIdAsync(id));

    [HttpPut]
    public async Task<IActionResult> ModifyAsync(long id, [FromForm] ProductForUpdateDto productForUpdate)
        => Ok(await _productService.UpdateAsync(id, productForUpdate));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([Required] long id)
        => Ok(await _productService.DeleteAsync(id));


}
