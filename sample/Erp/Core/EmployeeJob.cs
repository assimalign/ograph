namespace Erp;

public class EmployeeJob : EntityBase<EmployeeJob>
{
    public JobId? JobId { get; set; }
    public EmployeeId? EmployeeId { get; set; }
}
