using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

public class GdmEmployeeCreate : GdmComplexType<Employee>
{
    protected override void Configure(IOGraphGdmComplexTypeDescriptor<Employee> descriptor)
    {
        descriptor.HasName("EmployeeCreate");
    }
}
