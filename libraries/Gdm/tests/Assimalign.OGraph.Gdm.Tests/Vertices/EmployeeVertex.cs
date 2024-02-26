using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

internal class EmployeeVertex : GdmVertex<Employee>
{
    protected override void Configure(IOGraphGdmVertexDescriptor<Employee> descriptor)
    {
        descriptor.HasLabel("employee");
            //.HasEdge<EmployeeAddress>("addresses")
            //.HasEdge<EmployeeAddress>("primaryAddress")
            //.HasType(employee =>
            //{
            //    employee.HasKey(p => p.EmployeeId);
            //    employee.HasLabel
            //    employee.HasProperty(p => p.EmployeeId).UsePropertyName("employeeId");
            //    employee.HasProperty(p => p.Roles).UsePropertyName("roles")
            //        .UseType(role =>
            //        {
            //            role.HasLabel("EmployeeRole");
            //            role.HasProperty(p => p.Id).UsePropertyName("id");
            //        });
            //});
    }
}
