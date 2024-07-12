using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

public class EmployeeJobEntity : GdmEntityType<EmployeeJob>
{
    protected override void Configure(IOGraphGdmEntityTypeDescriptor<EmployeeJob> descriptor)
    {
        descriptor.HasLabel("employeeJob");
        descriptor.HasKey(p => p.JobId);


        descriptor.HasProperty(p => p.CreatedBy)
            .UsePropertyName("createdBy")
            .UseType<GdmAuditField>();

        descriptor.HasProperty(p => p.UpdatedBy)
            .UsePropertyName("updatedBy")
            .UseType<GdmAuditField>();
    }

}
