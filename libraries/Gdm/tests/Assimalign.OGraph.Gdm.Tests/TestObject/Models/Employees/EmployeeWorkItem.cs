using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

public class EmployeeWorkItem : EmployeeBase<EmployeeWorkItem>
{
    public WorkItemId? WorkItemId { get; set; }
    public JobId? JobId { get; set; }
    public TaskId? TaskId { get; set; }
    public EmployeeId? EmployeeId { get; set; }
}
