using System;

namespace Erp;

public class EmployeeTaxInfo : EntityBase<EmployeeTaxInfo> 
{
    public Guid? TaxInfoId { get; set; }
    public EmployeeId? EmployeeId { get; set; }
}
