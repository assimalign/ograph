using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

public class EmployeeAddressEntity : GdmEntityType<EmployeeAddress>
{
    protected override void Configure(IOGraphGdmEntityTypeDescriptor<EmployeeAddress> descriptor)
    {
        descriptor.HasLabel("employeeAddress");
        descriptor.HasKey(p => p.AddressId);


        descriptor.HasProperty(p => p.CreatedBy).UsePropertyName("createdBy").UseType<AuditFieldType>();
        descriptor.HasProperty(p => p.UpdatedBy).UsePropertyName("updatedBy").UseType<AuditFieldType>();
    }
}
