using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCore.Employees;

public class EmployeeAddress
{
    public AddressId Id { get; set; }
    public Address? Address { get; set; }
    public AuditField? Created { get; set; }
    public AuditField? Updated { get; set; }
}
