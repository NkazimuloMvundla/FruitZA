using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitZA.Domain.Entities;
public  class AuditAction : BaseAuditableEntity
{
    public AuditAction()
    {
        this.AuditActionLogs = new HashSet<AuditActionLog>();
    }
    public string? ActionName { get; set; }

    public virtual ICollection<AuditActionLog> AuditActionLogs { get; set; }
}
