using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FruitZA.Application.Common.Exceptions;
using FruitZA.Application.Common.Interfaces;
using FruitZA.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace FruitZA.Application.Products.Commands.CreateProduct;
public class ProcessProductExcelCommand : IRequest<Unit>
{
    public IFormFile? ExcelFile { get; set; }

    // Add any additional properties needed for processing the Excel file

    // You can also add validation logic here if necessary
}

/*public class ProcessProductExcelCommandValidator : AbstractValidator<ProcessProductExcelCommand>
{
    public ProcessProductExcelCommandValidator()
    {
        RuleFor(command => command.ExcelFile)
            .NotEmpty().WithMessage("Excel file is required.")
            .Must(BeAnExcelFile).WithMessage("Only Excel files are allowed.");
    }

    private bool BeAnExcelFile(IFormFile? file)
    {
        if (file == null)
            return false;

        // Check the file extension to ensure it's an Excel file
        return file.FileName.EndsWith(".xls") || file.FileName.EndsWith(".xlsx");
    }
}
*/

public class ProcessProductExcelCommandHandler : IRequestHandler<ProcessProductExcelCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public ProcessProductExcelCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(ProcessProductExcelCommand request, CancellationToken cancellationToken)
    {
        if (request.ExcelFile != null && !IsExcelFile(request.ExcelFile))
        {
            throw new FruitSAException("Only Excel files are allowed." );
        }


        if (request.ExcelFile != null)
        {
            using (var stream = new MemoryStream())
            {
                await request.ExcelFile.CopyToAsync(stream);
                byte[] excelBytes = stream.ToArray();

                // Save the byte array to the database or process it further as needed
                // For example, you can save it to a specific table in the database
                // or convert it to a model and process the data before saving

                // Example: Saving to a database table
                var uploadedExcel = new UploadedExcel
                {
                    Content = excelBytes,
                    ContentType = request.ExcelFile.ContentType,
                    FileName = request.ExcelFile.FileName,
                    // Add other properties if needed
                };

                _context.Resources.Add(uploadedExcel);
                await _context.SaveChangesAsync(cancellationToken);
            }

         
        }
        return Unit.Value;
    }

    private bool IsExcelFile(IFormFile file)
    {
        // Implement logic to check if the file is an Excel file
        if (file == null || file.Length == 0) return false;

        // Check file extension or content type to determine if it's an Excel file
        return file.FileName.EndsWith(".xls") || file.FileName.EndsWith(".xlsx");
    }

    /*    public async Task<Unit> Handle(ProcessProductExcelCommand request, CancellationToken cancellationToken)
        {
            if(request.ExcelFile != null)
            {
                // Read the uploaded Excel file
                using (var stream = new MemoryStream())
                {
                    await request.ExcelFile.CopyToAsync(stream);

                    // Load the Excel package
                    using (var package = new ExcelPackage(stream))
                    {
                        // Get the first worksheet
                        var worksheet = package.Workbook.Worksheets[0];

                        // Assume the first row contains column headers
                        var rowCount = worksheet.Dimension.End.Row;
                        var columnCount = worksheet.Dimension.End.Column;

                        // Iterate through rows starting from the second row (skip header)
                        for (int row = 2; row <= rowCount; row++)
                        {
                            // Read data from Excel and map it to your product model
                            var productCode = worksheet.Cells[row, 1].GetValue<string>();
                            var name = worksheet.Cells[row, 2].GetValue<string>();
                            var description = worksheet.Cells[row, 3].GetValue<string>();
                            var categoryName = worksheet.Cells[row, 4].GetValue<string>();
                            var price = worksheet.Cells[row, 5].GetValue<decimal>();

                            // Create or update product in the database
                            // Implement your logic to save the product to the database
                        }
                    }
                }
            }


            return Unit.Value;
        }*/
}
