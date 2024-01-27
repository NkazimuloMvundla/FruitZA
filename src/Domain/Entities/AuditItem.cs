using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitZA.Domain.Entities;
public class AuditItem :  BaseAuditableEntity
{
    public AuditItem()
    {
        this.AuditActionLogs = new HashSet<AuditActionLog>();
    }
    public string? FeatureName { get; set; }

    public virtual ICollection<AuditActionLog> AuditActionLogs { get; set; }
}
