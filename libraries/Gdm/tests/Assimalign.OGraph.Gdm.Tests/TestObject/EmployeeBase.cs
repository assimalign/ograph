using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public abstract record class EmployeeBase<T> 
{
    public AuditField? CreatedBy { get; set; }
    public AuditField? UpdatedBy { get; set; }
}
