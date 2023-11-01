using System;

namespace Assimalign.OGraph.Gdm;

public record class EmployeeTaxInfo : EmployeeBase<EmployeeTaxInfo> 
{
    public Guid? TaxInfoId { get; set; }
}
