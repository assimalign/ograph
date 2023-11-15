using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

internal class EmployeeVertex : GdmVertex<Employee>
{
    protected override void Configure(IOGraphGdmVertexDescriptor<Employee> descriptor)
    {
        descriptor.HasLabel("Employee");
        descriptor.HasType(descriptor =>
        {
            descriptor.HasKey(p => p.EmployeeId);
        });
    }
}
