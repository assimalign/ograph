using System;

namespace Assimalign.ErpCore.Entities;

/// <summary>
/// 
/// </summary>
public record class EmployeeAddress : DomainEntity<EmployeeAddress>
{
    private Address? address;

    /// <summary>
    /// 
    /// </summary>
    public Guid? AddressId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Guid? EmployeeId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Address? Address
    {
        get => address;
        set
        {
            BeginPropertyChange();
            address = value;
            EndPropertyChange();
        }
    }
}
