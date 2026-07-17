using Assimalign.OGraph.Gdm.Tests.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

using Elements;

public class EmployeeAddressEntity : GdmEntityType<EmployeeAddress>
{
    public EmployeeAddressEntity(GdmGraph graph) : base(graph)
    {
    }

    protected override void Configure(GdmEntityTypeDescriptor<EmployeeAddress> descriptor)
    {
        descriptor.HasName("employeeAddress");
        descriptor.HasKey(p => p.AddressId);


        descriptor.HasProperty(p => p.CreatedBy).UsePropertyName("createdBy").UseType<GdmAuditField>();
        descriptor.HasProperty(p => p.UpdatedBy).UsePropertyName("updatedBy").UseType<GdmAuditField>();
    }
}
