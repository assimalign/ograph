using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

public class EmployeeAddressTypeEntity : GdmEntityType<EmployeeAddressType>
{
    protected override void Configure(IOGraphGdmEntityTypeDescriptor<EmployeeAddressType> descriptor)
    {
        descriptor.HasName("employeeAddressType");
        descriptor.HasKey(p => p.TypeId);

        descriptor.HasProperty(p => p.CreatedBy).UsePropertyName("createdBy").UseType<GdmAuditField>();
        descriptor.HasProperty(p => p.UpdatedBy).UsePropertyName("updatedBy").UseType<GdmAuditField>();
    }
}
