using FruitZA.Application.Common.Interfaces;
using FruitZA.Domain.Entities;
using FruitZA.Domain.Events;
using Microsoft.AspNetCore.Http;

namespace FruitZA.Application.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<int>
{
    public required string ProductCode { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string CategoryName { get; set; }
    public decimal Price { get; set; }
    public IFormFile? image { get; set; }
}

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
    }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Check if product with the provided product code already exists
        var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.ProductCode.Equals(request.ProductCode));

        byte[]? imageBytes = null;

        if (existingProduct != null)
        {
            // If product exists, update its properties
            existingProduct.Name = request.Name;
            existingProduct.Description = request.Description;
            existingProduct.CategoryName = request.CategoryName;
            existingProduct.Price = request.Price;

            // Convert image to byte array if it exists
            if (request.image != null)
            {
                imageBytes = await ConvertFileToByteArray(request.image);
                existingProduct.Image = imageBytes;
            }

            // Save changes to the database
            await _context.SaveChangesAsync(cancellationToken);

            // Return the ID of the updated product
            return existingProduct.Id;
        }
        else
        {
            // If product does not exist, create a new one
            var productCode = await GetExistingProductsCode();

            // Convert image to byte array if it exists
            if (request.image != null)
            {
                imageBytes = await ConvertFileToByteArray(request.image);
            }

            // Create a new Product entity
            var entity = new Product
            {
                ProductCode = productCode,
                Name = request.Name,
                Description = request.Description,
                CategoryName = request.CategoryName,
                Price = request.Price,
                Image = imageBytes
            };

            // Add domain event
            entity.AddDomainEvent(new ProductCreatedEvent(entity));

            // Add the new product entity to the context
            _context.Products.Add(entity);

            // Save changes to the database
            await _context.SaveChangesAsync(cancellationToken);

            // Return the ID of the created product
            return entity.Id;
        }

    }

    private async Task<byte[]> ConvertFileToByteArray(IFormFile file)
    {
        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }


    //todo:move to it own file
 /*   private async Task<string> ConvertFileToBase64(IFormFile file)
    {
        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();
            return Convert.ToBase64String(fileBytes);
        }
    }*/
    private async Task<string> GetExistingProductsCode()
    {
        string? latestProductCode = null;
        string newProductCode;

        latestProductCode = await _context.Products
            .OrderByDescending(p => p.ProductCode) // Order by ProductCode in descending order to get the latest one first
            .Select(p => p.ProductCode) // Select only the ProductCode
            .FirstOrDefaultAsync(); // Get the first (latest) product code or default if no products exist

        if (latestProductCode != null)
        {
            // Records exist, and latestProductCode contains the latest product code
            // Extract year and month from the latest product code
            string lastYearMonth = latestProductCode.Substring(0, 6);

            // Increment the sequential code part
            int lastSequentialCode = int.Parse(latestProductCode.Substring(7));
            int newSequentialCode = lastSequentialCode + 1;

            // Generate the new product code
            newProductCode = $"{lastYearMonth}-{newSequentialCode.ToString("000")}";
        }
    
        else
        {
            // No records exist in the Products table, generate a new product code with the current year and month
            string currentYearMonth = DateTime.Now.ToString("yyyyMM");
            newProductCode = $"{currentYearMonth}-001"; // Start with sequential code 001
        }

        return newProductCode;
    }
}
