using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

internal class EmployeeEntity : GdmEntityType<Employee>
{
    protected override void Configure(IOGraphGdmEntityTypeDescriptor<Employee> descriptor)
    {
        descriptor.HasKey(p => p.EmployeeId);
    }
}
