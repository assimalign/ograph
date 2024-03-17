using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

public class EmployeeTaxInfoEntity : GdmEntityType<EmployeeTaxInfo>
{
    protected override void Configure(IOGraphGdmEntityTypeDescriptor<EmployeeTaxInfo> descriptor)
    {


        descriptor.HasProperty(p => p.CreatedBy).UsePropertyName("createdBy").UseType<AuditFieldType>();
        descriptor.HasProperty(p => p.UpdatedBy).UsePropertyName("updatedBy").UseType<AuditFieldType>();
    }
}
