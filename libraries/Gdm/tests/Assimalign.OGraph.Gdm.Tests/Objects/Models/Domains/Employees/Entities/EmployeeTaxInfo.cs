using System;

namespace Assimalign.OGraph.Gdm.Tests.Objects;

public class EmployeeTaxInfo : EmployeeBase<EmployeeTaxInfo> 
{
    public Guid? TaxInfoId { get; set; }
    public EmployeeId? EmployeeId { get; set; }
}
