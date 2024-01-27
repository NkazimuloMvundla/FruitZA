using FruitZA.Application.Categories.Commands.CreateCategory;
using FruitZA.Application.Categories.Queries.GetCategories;
using FruitZA.Application.Categories.Queries.GetCategoriesWithPagination;
using FruitZA.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace FruitZA.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetCategories")]
    public async Task<ActionResult<PaginatedList<CategoryDto>>> GetCategories([FromQuery] GetCategoriesWithPaginationQuery query)
    {
        var categories = await _mediator.Send(query);
        return Ok(categories);
    }

    [HttpPost("CreateCategory")]
    public async Task<IActionResult> CreateCategory(CreateOrUpdateCategoryCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
