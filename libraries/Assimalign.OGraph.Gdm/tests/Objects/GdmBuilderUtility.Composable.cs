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
        var model = new Gdm();

        // Employees Graph
        var graph = new GdmGraph("Employees", model);
        var employeeType = new GdmEntityType("Employee", graph);

        var booleanType = new GdmBooleanType(graph);
        var stringType = new GdmStringType(graph);
        var uuidType = new GdmUuidType(graph);


        model.Graphs.Add(graph);

        return model;
    }
}
