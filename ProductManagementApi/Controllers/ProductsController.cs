using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementBusiness.Dtos.Product;

using ProductManagementBusiness.Interfaces.Managers;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]

public class ProductsController : ControllerBase
{
    private readonly IProductManager _productManager;

    public ProductsController(IProductManager productManager)
    {
        _productManager = productManager;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]   
    public async Task<IActionResult> Create( ProductCreateDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _productManager.CreateProductAsync(dto, userId);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productManager.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productManager.GetAllProductsAsync();
        return Ok(products);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]   
    public async Task<IActionResult> Update(int id,ProductUpdateDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _productManager.UpdateProductAsync(dto, userId);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]    
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _productManager.DeleteProductAsync(id, userId);
        return NoContent();
    }
}