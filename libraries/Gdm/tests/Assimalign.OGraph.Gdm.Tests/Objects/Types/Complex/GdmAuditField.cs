using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

using Elements;

public class GdmAuditField : GdmComplexType<AuditField>
{
    public GdmAuditField(GdmGraph graph) : base(graph)
    {
    }

    protected override void Configure(GdmComplexTypeDescriptor<AuditField> descriptor)
    {
        descriptor.HasName("EmployeeAuditField");
        descriptor.HasProperty(p => p.UserId).UsePropertyName("userId");
        descriptor.HasProperty(p => p.Timestamp).UsePropertyName("timestamp");
    }
}
