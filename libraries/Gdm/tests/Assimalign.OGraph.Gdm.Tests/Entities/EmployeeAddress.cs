using System;

namespace Assimalign.OGraph.Gdm;

public record class EmployeeAddress : EmployeeBase<EmployeeAddress>
{
    public EmployeeId? EmployeeId { get; set; }
    public Guid? AddressId { get; set; }
    public Address? Address { get; set; }
}
