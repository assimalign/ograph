namespace Assimalign.OGraph.Gdm;

public class Employee : EmployeeBase
{
    public EmployeeKind? Kind { get; set; }
    public EmployeeDetails? Details { get; set; }
    public IEnumerable<EmployeeRole>? Roles { get; set; }
}
