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
        
        var model = TestUtility.CreateFluentOnlyModel();

        var vertices = model.GetGdmVertices();
        var types = model.GetGdmTypes();
        var primitives = model.GetGdmPrimitiveTypes();
        var entities = model.GetGdmEntityTypes();
        var collection = model.GetGdmCollectionTypes();

        var writer = new Utf8JsonWrite

    }
}