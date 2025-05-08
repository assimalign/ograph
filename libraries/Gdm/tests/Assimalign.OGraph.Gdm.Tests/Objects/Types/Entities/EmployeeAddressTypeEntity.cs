using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

using Elements;
using Objects;

public class EmployeeAddressTypeEntity : GdmEntityType<EmployeeAddressType>
{
    public EmployeeAddressTypeEntity(GdmGraph graph) : base(graph)
    {
    }

    protected override void Configure(GdmEntityTypeDescriptor<EmployeeAddressType> descriptor)
    {
        descriptor.HasName("employeeAddressType");
        descriptor.HasKey(p => p.TypeId);

        descriptor.HasProperty(p => p.CreatedBy).UsePropertyName("createdBy").UseType<GdmAuditField>();
        descriptor.HasProperty(p => p.UpdatedBy).UsePropertyName("updatedBy").UseType<GdmAuditField>();
    }
}
