using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FruitZA.Domain.Entities;

namespace FruitZA.Application.Products.Queries.GetExcelFiles;

public class ExcelFileDto
{
    public int Id { get; set; }
    public required string FileName { get; set; } // Name of the uploaded file
    public required string ContentType { get; set; } // MIME type of the uploaded file
    public required byte[] Content { get; set; } // Byte array representing the content of the uploaded file
    public DateTime UploadedAt { get; set; } // Date and time when the file was uploaded
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UploadedExcel, ExcelFileDto>();
        }
    }
}
