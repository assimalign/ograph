using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Tests;

public partial class OGraphBuilderTests
{

    [Fact(DisplayName = "")]
    public void TestOGraphAddNodeInstance()
    {
        var ograph = OGraphBuilder.Create("Test", builder =>
        {
            //builder.AddNode();
        });

        Assert.Equal(1, ograph.Vertices.Count);

    }
}
