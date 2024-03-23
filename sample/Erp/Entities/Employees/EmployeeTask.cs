namespace Erp;

public class EmployeeTask : EntityBase<EmployeeTask>
{
    public TaskId? TaskId { get; set; }
    public JobId? JobId { get; set; }
    public EmployeeId? EmployeeId { get; set; }
}
