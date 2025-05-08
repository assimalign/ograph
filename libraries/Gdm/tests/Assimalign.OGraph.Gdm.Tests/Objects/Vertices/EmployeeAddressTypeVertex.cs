using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

using Assimalign.OGraph.Gdm.Tests.Objects;
using Elements;

public class EmployeeAddressTypeVertex : GdmVertex<EmployeeAddressType>
{
    public EmployeeAddressTypeVertex(GdmGraph graph) : base(graph)
    {
        
    }
    protected override void Configure(GdmVertexDescriptor<EmployeeAddressType> descriptor)
    {
        descriptor.HasLabel("addressType");
    }
}
