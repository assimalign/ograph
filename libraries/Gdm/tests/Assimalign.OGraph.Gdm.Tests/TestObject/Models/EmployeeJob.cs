using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

public class EmployeeJob : EmployeeBase<EmployeeJob>
{
    public JobId? JobId { get; set; }
    public EmployeeId? EmployeeId { get; set; }
}
