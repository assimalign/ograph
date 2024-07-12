using System.Collections.Generic;

namespace Erp;


public class Employee : DomainEntity<Employee, EmployeeId>
{
    public EmployeeKind? Kind { get; set; }
    public EmployeeInfo? Info { get; set; }
    public IEnumerable<EmployeeRole>? Roles { get; set; }
    public Audit? Created { get; set; }
    public Audit? Updated { get; set; }
    public override Domain Domain => Domain.Employees;
    public override DomainEntityType EntityType => DomainEntityType.Employee;
}
