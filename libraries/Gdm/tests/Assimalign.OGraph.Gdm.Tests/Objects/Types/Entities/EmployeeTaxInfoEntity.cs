using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

using Elements;
using Objects;

public class EmployeeTaxInfoEntity : GdmEntityType<EmployeeTaxInfo>
{
    public EmployeeTaxInfoEntity(GdmGraph graph) : base(graph)
    {
    }

    protected override void Configure(GdmEntityTypeDescriptor<EmployeeTaxInfo> descriptor)
    {


        descriptor.HasProperty(p => p.CreatedBy).UsePropertyName("createdBy").UseType<GdmAuditField>();
        descriptor.HasProperty(p => p.UpdatedBy).UsePropertyName("updatedBy").UseType<GdmAuditField>();
    }
}
