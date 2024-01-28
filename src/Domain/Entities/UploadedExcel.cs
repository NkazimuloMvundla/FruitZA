using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FruitZA.Domain.Entities;


public class UploadedExcel : BaseAuditableEntity
{
    public required string FileName { get; set; } // Name of the uploaded file
    public required string ContentType { get; set; } // MIME type of the uploaded file
    public required byte[] Content { get; set; } // Byte array representing the content of the uploaded file
    public DateTime UploadedAt { get; set; } // Date and time when the file was uploaded
    // Add other properties as needed

    // Optional: Navigation properties if related to other entities
    // Example: public ICollection<Product> Products { get; set; }
}
