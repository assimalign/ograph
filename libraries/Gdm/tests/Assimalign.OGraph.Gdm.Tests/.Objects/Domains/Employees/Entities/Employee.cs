using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Tests.Objects;

public class Employee : EmployeeBase<Employee>
{
    public EmployeeId? EmployeeId { get; set; }
    public EmployeeKind? Kind { get; set; }
    public EmployeeInfo? Info { get; set; }
    public IEnumerable<EmployeeRole>? Roles { get; set; }
}
