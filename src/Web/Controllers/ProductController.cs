using FruitZA.Application.Common.Models;
using FruitZA.Application.Common.Security;
using FruitZA.Application.Products.Commands.CreateProduct;
using FruitZA.Application.Products.Commands.DeleteProduct;
using FruitZA.Application.Products.Queries.GetExcelFiles;
using FruitZA.Application.Products.Queries.GetProductsWithPagination;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FruitZA.Web.Controllers;

//[Authorize] // Controllers that mainly require Authorization still use Controller/View; other pages use Pages

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{

    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("CreateProduct")]
/*    [ValidateAntiForgeryToken]*/
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

    [HttpDelete("DeleteProduct/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var command = new DeleteProductCommand { ProductId = id };
        await _mediator.Send(command);
        return NoContent();
    }



    [HttpPost("UpdloadExcel")]
    public async Task<IActionResult> UpdloadExcel(ProcessProductExcelCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet("UploadedExcelFiles")]
    public async Task<ActionResult<PaginatedList<ExcelFileDto>>> GetUploadedExcelFiles(
           [FromQuery] GetUploadedExcelFilesQuery query)
    {
        var uploadedExcelFiles = await _mediator.Send(query);
        return Ok(uploadedExcelFiles);
    }

    [HttpGet("DownloadUploadedExcel")]
    public async Task<IActionResult> DownloadUploadedExcel([FromQuery] DownloadUploadedProductExcelQuery query)
    {
        var excelBytes = await _mediator.Send(query);
        return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "uploaded_products.xlsx");
    }


}
