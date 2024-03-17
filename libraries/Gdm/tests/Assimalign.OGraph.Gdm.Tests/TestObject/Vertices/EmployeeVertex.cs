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
            .HasType<EmployeeEntity>();

        descriptor.HasEdge<EmployeeAddressVertex>("addresses");
        descriptor.HasEdge<EmployeeJobVertex>("jobs");
    }
}
