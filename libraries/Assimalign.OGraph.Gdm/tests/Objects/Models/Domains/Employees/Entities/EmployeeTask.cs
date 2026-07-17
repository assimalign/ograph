using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests.Objects;

public class EmployeeTask : EmployeeBase<EmployeeTask>
{
    public TaskId? TaskId { get; set; }
    public JobId? JobId { get; set; }
    public EmployeeId? EmployeeId { get; set; }
}
