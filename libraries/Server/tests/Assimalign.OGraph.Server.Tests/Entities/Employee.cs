using System.Collections.Generic;

namespace Assimalign.OGraph.Server;

public record class Employee : EmployeeBase<Employee>
{
    public EmployeeId? EmployeeId { get; set; }
    public EmployeeKind? Kind { get; set; }
    public EmployeeDetails? Details { get; set; }
    public IEnumerable<EmployeeRole>? Roles { get; set; }
}
