using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitZA.Domain.Entities;
// Product entity
public class Product : BaseAuditableEntity
{
    public required string ProductCode { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string CategoryName { get; set; }
    public decimal Price { get; set; }
    public byte[]? Image { get; set; }
}
