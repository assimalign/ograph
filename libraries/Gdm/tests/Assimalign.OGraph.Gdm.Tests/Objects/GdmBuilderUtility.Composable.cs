using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

using Elements;
using Objects;

public static partial class GdmBuilderUtility
{
    private static IOGraphGdm CreateComposableModel()
    {
        var gdm = new Gdm("ErpCore");

        // Employees Graph
        var employees = new GdmGraph("Employees", gdm);
        
        employees.Types.Add(new GdmBooleanType());

        var entity1 = GdmEntityType.Create<Employee>("EmployeeId", employees);
        var entity = new GdmEntityType("EmployeeId", typeof(Employee), employees);


        // Organization Graph
        var organization = new GdmGraph("Organization", gdm);
        
        
        gdm.Graphs.Add(employees);
        gdm.Graphs.Add(organization);

        return gdm;
    }
}
