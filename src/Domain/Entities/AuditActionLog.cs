using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitZA.Domain.Entities;
public class AuditActionLog  :  BaseAuditableEntity
{
    public int AuditItemId { get; set; }
    public System.DateTime DateChanged { get; set; }
    public string? UserId { get; set; }
    public string? Value { get; set; }
    public string? AttributeChanged { get; set; }
}
