using System.Collections.Generic;

namespace Erp;

public class Employee : EntityBase<Employee>
{
    public EmployeeId? EmployeeId { get; set; }
    public EmployeeKind? Kind { get; set; }
    public EmployeeInfo? Info { get; set; }
    public IEnumerable<EmployeeRole>? Roles { get; set; }
}
