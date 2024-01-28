using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FruitZA.Application.Common.Exceptions;
using FruitZA.Application.Common.Exceptions.Constants;
using FruitZA.Application.Common.Interfaces;
using FruitZA.Application.Common.Security;
using FruitZA.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace FruitZA.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand : IRequest<bool>
{
    public required int ProductId { get; set; }
}
[Authorize(Roles = "ProductManager")]
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.ProductId);

        if (product == null)
        {
           // throw new NotFoundException(nameof(Product), request.ProductId.ToString());
            throw new FruitSAException(ConstantsException.ProductNotFound,System.Net.HttpStatusCode.NotFound);
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
