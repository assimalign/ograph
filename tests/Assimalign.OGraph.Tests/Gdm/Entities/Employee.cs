namespace Assimalign.OGraph.Gdm;

public record class Employee : EmployeeBase<Employee>
{
    public EmployeeKind? Kind { get; set; }
    public EmployeeDetails? Details { get; set; }
    public IEnumerable<EmployeeRole>? Roles { get; set; }
}
