using System;

namespace Assimalign.ErpCore.Entities;

/// <summary>
/// 
/// </summary>
public record class Employee : DomainEntity<Employee>
{
    private EmployeeDetails? details;

    /// <summary>
    /// 
    /// </summary>
    public EmployeeId? EmployeeId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public EmployeeDetails? Details
    {
        get => this.details;
        set
        {
            BeginPropertyChange();
            this.details = value;
            EndPropertyChange();
        }
    }
}
