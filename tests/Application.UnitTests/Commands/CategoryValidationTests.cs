using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FruitZA.Application.Categories.Commands.CreateCategory;
using NUnit.Framework;


[TestFixture]
public class CategoryValidationTests
{
/*    [TestCase("ABC123", true)] // Valid category code
    [TestCase("abc123", true)] // Valid category code with lowercase letters
    [TestCase("123ABC", false)] // Invalid category code: Starts with numeric characters
    [TestCase("AB123C", false)] // Invalid category code: Contains numeric characters in the middle
    [TestCase("ABC12", false)] // Invalid category code: Less than 6 characters
    [TestCase("ABC1234", false)] // Invalid category code: More than 6 characters
    [TestCase("ABCD23", false)] // Invalid category code: Contains alphabetic characters more than 3
    [TestCase("123456", false)] // Invalid category code: Contains only numeric characters
    public void ValidateCategoryCode_WithVariousCategoryCodes_ReturnsExpectedResult(string categoryCode, bool expectedResult)
    {
        // Arrange
        var handler = new CreateOrUpdateCategoryCommandHandler(null); // Pass null for context since we're only testing validation
        var request = new CreateOrUpdateCategoryCommand
        {
            CategoryCode = categoryCode
        };

        // Act
        var isValid = handler.ValidateCategoryCode(request);

        // Assert
        Assert.AreEqual(expectedResult, isValid);
    }*/

}
