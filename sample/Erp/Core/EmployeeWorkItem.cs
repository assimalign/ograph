using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp;

public class EmployeeWorkItem : EntityBase<EmployeeWorkItem>
{
    public WorkItemId? WorkItemId { get; set; }
    public JobId? JobId { get; set; }
    public TaskId? TaskId { get; set; }
    public EmployeeId? EmployeeId { get; set; }
}
