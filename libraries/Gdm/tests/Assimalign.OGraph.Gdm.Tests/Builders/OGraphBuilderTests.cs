using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Nodes;
using Xunit;

namespace Assimalign.OGraph.Gdm.Tests;

public partial class OGraphBuilderTests
{
    [Fact]
    public void Test1()
    {
        
        var model = GdmTestUtility.CreateFluentOnlyModel();

        var vertices = model.GetGdmVertices();
        var types = model.GetGdmTypes();
        var primitives = model.GetGdmPrimitiveTypes();
        var entities = model.GetGdmEntityTypes();
        var collection = model.GetGdmCollectionTypes();


        var stringType1 = types.First(p => p.Label == "Guid");
        var stringType2 = entities.First(p => p.Label == "employee").Properties.First(p => p.Label == "employeeId").Type.Definition;

        Assert.Equal(stringType1, stringType2);
        Assert.True(object.ReferenceEquals(stringType1, stringType2));
    }
}