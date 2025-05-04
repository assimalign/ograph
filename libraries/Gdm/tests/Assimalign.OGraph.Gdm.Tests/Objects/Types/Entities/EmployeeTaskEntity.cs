using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

public class EmployeeTaskEntity : GdmEntityType<EmployeeTask>
{
    protected override void Configure(IOGraphGdmEntityTypeDescriptor<EmployeeTask> descriptor)
    {
        descriptor.HasName("employeeJobTask");
        descriptor.HasKey(p => p.TaskId);

        descriptor.HasProperty(p => p.CreatedBy).UsePropertyName("createdBy").UseType<GdmAuditField>();
        descriptor.HasProperty(p => p.UpdatedBy).UsePropertyName("updatedBy").UseType<GdmAuditField>();
    }
}
