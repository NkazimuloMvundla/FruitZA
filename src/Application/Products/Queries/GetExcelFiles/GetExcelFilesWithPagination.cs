using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FruitZA.Application.Common.Interfaces;
using FruitZA.Application.Common.Mappings;
using FruitZA.Application.Common.Models;

namespace FruitZA.Application.Products.Queries.GetExcelFiles;
public record GetUploadedExcelFilesQuery : IRequest<PaginatedList<ExcelFileDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetUploadedExcelFilesQueryHandler : IRequestHandler<GetUploadedExcelFilesQuery, PaginatedList<ExcelFileDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUploadedExcelFilesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ExcelFileDto>> Handle(GetUploadedExcelFilesQuery request, CancellationToken cancellationToken)
    {
        var uploadedExcelFiles = _context.Resources
            .OrderBy(x => x.Id)
            .ProjectTo<ExcelFileDto>(_mapper.ConfigurationProvider);

        return await uploadedExcelFiles.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
