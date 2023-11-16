using System;

namespace Assimalign.OGraph.Server;

public record class EmployeeTaxInfo : EmployeeBase<EmployeeTaxInfo> 
{
    public Guid? TaxInfoId { get; set; }
    public EmployeeId? EmployeeId { get; set; }
}
