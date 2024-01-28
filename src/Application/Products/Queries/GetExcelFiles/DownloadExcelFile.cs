using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FruitZA.Application.Common.Interfaces;

namespace FruitZA.Application.Products.Queries.GetExcelFiles;
public record DownloadUploadedProductExcelQuery : IRequest<byte[]>
{
    public int fileId { get; set; }
}
public class DownloadUploadedProductExcelQueryHandler : IRequestHandler<DownloadUploadedProductExcelQuery, byte[]>
{
    private readonly IApplicationDbContext _context;

    public DownloadUploadedProductExcelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<byte[]> Handle(DownloadUploadedProductExcelQuery request, CancellationToken cancellationToken)
    {
        // Retrieve the uploaded Excel file from the database
        var uploadedExcelFile = await _context.Resources.FindAsync(request.fileId);

        if (uploadedExcelFile == null)
        {
            // Handle case where the Excel file is not found
            throw new Exception("Uploaded Excel file not found.");
        }

        // Convert the file data to byte array and return it for download
        byte[] excelFileBytes = uploadedExcelFile.Content; // Assuming FileData is the byte array property in your entity

        return excelFileBytes;
    }
}

