using FruitZA.Application.Common.Interfaces;
using FruitZA.Application.Common.Mappings;
using FruitZA.Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FruitZA.Application.Products.Queries.CreateProduct;

namespace FruitZA.Application.Products.Queries.GetProductsWithPagination
{
    public record GetProductsWithPaginationQuery : IRequest<PaginatedList<ProductPaginationDto>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, PaginatedList<ProductPaginationDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ProductPaginationDto>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.Products
                .OrderBy(x => x.Created) // Or any other ordering criteria you prefer
                .ProjectTo<ProductPaginationDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return data;
        }
    }
}
