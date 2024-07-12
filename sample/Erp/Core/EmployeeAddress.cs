using System;

namespace Erp;

public class EmployeeAddress : DomainEntity<EmployeeAddress, EmployeeAddressId>
{
    public EmployeeId EmployeeId { get; set; }
    public Address? Address { get; set; }
    public override Domain Domain => Domain.Employees;
    public override DomainEntityType EntityType => DomainEntityType.EmployeeAddress;
}
