using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FruitZA.Application.Common.Exceptions.Constants;
public static class ConstantsException
{
    // Error messages for various scenarios
    public const string ProductNotFound = "The specified product does not exist.";
    public const string ProductCreationFailed = "Failed to create the product.";
    public const string CategoryCreationFailed = "Failed to create the category.";

    // HTTP status codes for different scenarios
    public const HttpStatusCode ProductNotFoundStatusCode = HttpStatusCode.NotFound;
    public const HttpStatusCode DefaultErrorCode = HttpStatusCode.InternalServerError;
}
