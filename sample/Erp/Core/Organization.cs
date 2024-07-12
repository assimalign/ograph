using System;

namespace Erp.Entities;

public class Organization : EntityBase<Organization>
{
    public Guid? OrganizationId { get; set; }
}
