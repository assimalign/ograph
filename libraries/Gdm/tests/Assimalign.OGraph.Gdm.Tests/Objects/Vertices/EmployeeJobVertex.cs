using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

using Elements;
using Objects;

public class EmployeeJobVertex : GdmNode<EmployeeJob>
{
    public EmployeeJobVertex(GdmGraph graph) : base(graph)
    {
    }

    protected override void Configure(GdmVertexDescriptor<EmployeeJob> descriptor)
    {
        descriptor.HasLabel("employeeJob");
        descriptor.HasEntityType("")
        //descriptor.HasType<EmployeeJobEntity>();
    }
}
