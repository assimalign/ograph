using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests.Objects;

public abstract class EmployeeBase<T> 
{
    public Audit? CreatedBy { get; set; }
    public Audit? UpdatedBy { get; set; }
}
