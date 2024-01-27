using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitZA.Domain.Entities;
public class Category: BaseAuditableEntity
{
    public required string Name { get; set; }
    public required string CategoryCode { get; set; }
    public bool IsActive { get; set; }
}
