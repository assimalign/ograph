using System;

namespace Assimalign.ErpCore.Entities;

public record class Organization : DomainEntity<Organization>
{
    private OrganizationInfo? info;

    /// <summary>
    /// 
    /// </summary>
    public Guid? OrganizationId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public OrganizationInfo? Info
    {
        get => info;
        set
        {
            BeginPropertyChange();
            info = value;
            EndPropertyChange();
        }
    }
}
