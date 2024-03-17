using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

public class EmployeeJobVertex : GdmVertex<EmployeeJob>
{
    protected override void Configure(IOGraphGdmVertexDescriptor<EmployeeJob> descriptor)
    {
        descriptor.HasLabel("employeeJob");
        descriptor.HasType<EmployeeJobEntity>();
    }
}
