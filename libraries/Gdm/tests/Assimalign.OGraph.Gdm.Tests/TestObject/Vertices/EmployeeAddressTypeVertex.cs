using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

public class EmployeeAddressTypeVertex : GdmVertex<EmployeeAddressType>
{
    protected override void Configure(IOGraphGdmVertexDescriptor<EmployeeAddressType> descriptor)
    {
        descriptor.HasLabel("addressType")
            .HasType<EmployeeAddressTypeEntity>();
    }
}
