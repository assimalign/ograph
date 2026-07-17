using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

using Elements;
using Objects;

public class GdmEmployeeCreate : GdmComplexType<Employee>
{
    public GdmEmployeeCreate(GdmGraph graph) : base(graph)
    {
    }

    protected override void Configure(GdmComplexTypeDescriptor<Employee> descriptor)
    {
        descriptor.HasName("EmployeeCreate");
    }
}
