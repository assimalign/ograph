using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

public class GdmAuditField : GdmComplexType<AuditField>
{
    protected override void Configure(IOGraphGdmComplexTypeDescriptor<AuditField> descriptor)
    {
        descriptor.HasName("EmployeeAuditField");
        descriptor.HasProperty(p => p.UserId).UsePropertyName("userId");
        descriptor.HasProperty(p => p.Timestamp).UsePropertyName("timestamp");
    }
}
