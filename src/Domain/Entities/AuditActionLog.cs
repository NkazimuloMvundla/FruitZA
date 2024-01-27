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
    public Nullable<int> UserId { get; set; }
    public string? NewValue { get; set; }
    public string? OldValue { get; set; }
    public string? AttributeChanged { get; set; }

    public virtual AuditAction? AuditAction { get; set; }
    public virtual AuditItem? AuditItem { get; set; }
}
