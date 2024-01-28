using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FruitZA.Application.Categories.Commands.CreateCategory;
using FruitZA.Application.Common.Interfaces;
using FruitZA.Domain.Entities;
using FruitZA.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace FruitZA.Application.UnitTests.Commands;
[TestFixture]
public class CategoryIntegrationTests
{
    private DbContextOptions<ApplicationDbContext> _options;

    [SetUp]
    public void Setup()
    {
        // Configure the options for the in-memory database
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        // Seed the database with initial data if necessary
        using (var context = new ApplicationDbContext(_options))
        {
            // Add any initial data needed for the tests
            context.Categories.Add(new Category { CategoryCode = "InitialCode", Name = "InitialName" });
            context.SaveChanges();
        }
    }

    [Test]
    public async Task CreateCategory_ShouldAddNewCategoryToDatabase()
    {
        // Arrange
        var newCategory = new CreateOrUpdateCategoryCommand
        {
            Id = default,
            CategoryCode = "ABC123",
            Name = "NewCategoryName"
        };

        // Act
        using (var context = new ApplicationDbContext(_options))
        {
            var handler = new CreateOrUpdateCategoryCommandHandler(context);
            var categoryId = await handler.Handle(newCategory, default);
        }

        // Assert
        using (var context = new ApplicationDbContext(_options))
        {
            var createdCategory = await context.Categories.FirstOrDefaultAsync(c => c.CategoryCode == newCategory.CategoryCode);
            Assert.IsNotNull(createdCategory);
            Assert.That(createdCategory.Name, Is.EqualTo(newCategory.Name));
            // Add more assertions as needed
        }
    }
}
