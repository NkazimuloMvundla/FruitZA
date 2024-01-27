using FruitZA.Application.Common.Models;
using FruitZA.Application.Common.Security;
using FruitZA.Application.Products.Commands.CreateProduct;
using FruitZA.Application.Products.Queries.GetProductsWithPagination;
using FruitZA.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FruitZA.Web.Controllers;

//[Authorize] // Controllers that mainly require Authorization still use Controller/View; other pages use Pages
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{

    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateProduct")]
   // [ValidateAntiForgeryToken] gives a 400 bad request
    public async Task<IActionResult> CreateProduct(CreateProductCommand command)
    {
        // Send the command using MediatR
        await _mediator.Send(command);

        // You can return a response, or Ok if successful
        return Ok();
    }


    [HttpGet("GetProducts")]
    public async Task<ActionResult<PaginatedList<ProductPaginationDto>>> GetProducts(ISender sender, [FromQuery] GetProductsWithPaginationQuery query)
    {
        var products = await _mediator.Send(query);
        return Ok(products);
    }



}
