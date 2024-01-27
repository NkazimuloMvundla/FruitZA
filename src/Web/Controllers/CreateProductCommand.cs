namespace FruitZA.Web.Controllers;

public class CreateProduct
{
    public required string ProductCode { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string CategoryName { get; set; }
    public decimal Price { get; set; }
    public IFormFile? File { get; set; }
}
