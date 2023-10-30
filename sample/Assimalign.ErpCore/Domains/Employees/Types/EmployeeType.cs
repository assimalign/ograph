using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ErpCore.Types;

using Assimalign.OGraph;
using Assimalign.ErpCore.Entities;


public class EmployeeType : ComplexType<Employee>
{
    protected override void Configure(IOGraphComplexTypeDescriptor<Employee> descriptor)
    {
        
    }
}
