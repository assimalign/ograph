using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

public class EmployeeVertex : GdmVertex<Employee>
{
    protected override void Configure(IOGraphGdmVertexDescriptor<Employee> descriptor)
    {
        descriptor.HasLabel("employee")
            .HasType<GdmEmployeeEntity>();

        descriptor.HasEdge<EmployeeAddressVertex>("addresses");
        descriptor.HasEdge<EmployeeAddressVertex>("primaryAddress");
        descriptor.HasEdge<EmployeeJobVertex>("jobs");
    }
}
