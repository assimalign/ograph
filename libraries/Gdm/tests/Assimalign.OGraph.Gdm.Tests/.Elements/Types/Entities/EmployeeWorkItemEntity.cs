using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

public class EmployeeWorkItemEntity : GdmEntityType<EmployeeWorkItem>
{
    protected override void Configure(IOGraphGdmEntityTypeDescriptor<EmployeeWorkItem> descriptor)
    {
        descriptor.HasName("employeeWorkItem");
        descriptor.HasKey(p => p.WorkItemId);

        descriptor.HasProperty(p => p.CreatedBy).UsePropertyName("createdBy").UseType<GdmAuditField>();
        descriptor.HasProperty(p => p.UpdatedBy).UsePropertyName("updatedBy").UseType<GdmAuditField>();
    }
}
