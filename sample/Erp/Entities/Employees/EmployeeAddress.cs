using System;

namespace Erp;

public class EmployeeAddress : EntityBase<EmployeeAddress>
{
    public EmployeeId? EmployeeId { get; set; }
    public Guid? AddressId { get; set; }
    public Address? Address { get; set; }
}
