using System;

namespace Assimalign.OGraph.Gdm.Tests;

public class EmployeeAddress : EmployeeBase<EmployeeAddress>
{
    public EmployeeId? EmployeeId { get; set; }
    public Guid? AddressId { get; set; }
    public Address? Address { get; set; }
}
