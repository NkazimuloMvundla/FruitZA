using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FruitZA.Application.Common.Exceptions;
/// <summary>
/// Exception that bypasses exception filtering and will return the message to the client.
/// </summary>
[Serializable]
public class FruitSAException : ApplicationException
{
    public const string DefaultErrorMessage = "An unhandled error has occurred, please try again or contact the administrator.";

    public FruitSAException()
    {
    }

    public FruitSAException(string message)
        : base(message)
    {
    }

    public FruitSAException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public FruitSAException(string message, HttpStatusCode statusCode)
        : base(message)
    {
        this.HttpStatusCode = statusCode;
    }

    public HttpStatusCode? HttpStatusCode { get; set; }
}
