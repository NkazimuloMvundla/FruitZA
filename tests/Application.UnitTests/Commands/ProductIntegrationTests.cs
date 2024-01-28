using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FruitZA.Application.Common.Interfaces;
using FruitZA.Application.Products.Commands.CreateProduct;
using FruitZA.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace FruitZA.Application.UnitTests.Commands;
[TestFixture]
public class ProductIntegrationTests
{
    private DbContextOptions<ApplicationDbContext> _options;
    private Mock<IUser> _user = null!;

    [SetUp]
    public void Setup()
    {
        _user = new Mock<IUser>();
        // Configure the options for the in-memory database
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Test]
    public async Task Handle_CreateOrUpdateProduct_ShouldCreateOrUpdateProduct()
    {
        // Arrange
        var request = new CreateProductCommand
        {
            ProductCode = "ABC123", // Assuming a unique product code
            Name = "TestProduct",
            Description = "Test description",
            CategoryName = "TestCategory",
            Price = 10.99m,
            image = null // Assuming no image for this test
        };

        var cancellationToken = CancellationToken.None;

        using (var context = new ApplicationDbContext(_options))
        {
            // Seed the database with any required data
            // For this test, there's no need to seed data as we're creating a new product
        }

        using (var context = new ApplicationDbContext(_options))
        {
            var userID = _user.Setup(x => x.Id).Returns(Guid.NewGuid().ToString());

            var handler = new CreateProductCommandHandler(context, (IUser)userID);

            // Act
            var productId = await handler.Handle(request, cancellationToken);

            // Assert
            Assert.Greater(productId, 0); // Ensure a valid product ID is returned

            // Verify product is added to the database
            var createdProduct = await context.Products.FirstOrDefaultAsync(p => p.ProductCode == request.ProductCode);
            Assert.IsNotNull(createdProduct); // Ensure product is not null
            Assert.That(createdProduct.Name, Is.EqualTo(request.Name)); // Ensure product name matches
            Assert.That(createdProduct.Description, Is.EqualTo(request.Description)); // Ensure description matches
            Assert.That(createdProduct.CategoryName, Is.EqualTo(request.CategoryName)); // Ensure category name matches
            Assert.That(createdProduct.Price, Is.EqualTo(request.Price)); // Ensure price matches
                                                            
        }
    }
}
