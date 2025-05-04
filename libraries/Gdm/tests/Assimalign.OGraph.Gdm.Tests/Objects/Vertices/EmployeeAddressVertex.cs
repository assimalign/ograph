using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

public class EmployeeAddressVertex : GdmVertex<EmployeeAddress>
{
    protected override void Configure(IOGraphGdmVertexDescriptor<EmployeeAddress> descriptor)
    {
        descriptor.HasLabel("address")
            .HasType<EmployeeAddressEntity>();
    }
}
