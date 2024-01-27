using FruitZA.Application.Common.Interfaces;
using FruitZA.Application.Common.Models;
using FruitZA.Application.Common.Security;
using FruitZA.Domain.Enums;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FruitZA.Application.Products.Queries.CreateProduct;

namespace FruitZA.Application.Products.Queries.GetProducts
{
   // [Authorize] // Optional authorization attribute
    public record GetProductsQuery : IRequest<ProductsVm>;

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductsVm> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .AsNoTracking()
                .OrderByDescending(p => p.Created) // Assuming there's a CreatedAt property for products
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ProductsVm { Products = products };
        }
    }
}

