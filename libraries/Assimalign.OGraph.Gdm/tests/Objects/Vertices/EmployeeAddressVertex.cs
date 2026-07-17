using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

using Assimalign.OGraph.Gdm.Tests.Objects;
using Elements;

public class EmployeeAddressVertex : GdmNode<EmployeeAddress>
{
    public EmployeeAddressVertex(GdmGraph graph) : base(graph)
    {
    }

    protected override void Configure(GdmVertexDescriptor<EmployeeAddress> descriptor)
    {
        descriptor.HasLabel("address");
    }
}
