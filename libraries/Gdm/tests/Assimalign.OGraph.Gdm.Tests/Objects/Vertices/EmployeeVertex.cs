using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

using Elements;
using Objects;

public class EmployeeVertex : GdmVertex<Employee>
{
    public EmployeeVertex(GdmGraph graph) : base(graph)
    {
    }

    protected override void Configure(GdmVertexDescriptor<Employee> descriptor)
    {
        descriptor.HasLabel("employee");
        //descriptor.HasEntityType(default);
    }
}
