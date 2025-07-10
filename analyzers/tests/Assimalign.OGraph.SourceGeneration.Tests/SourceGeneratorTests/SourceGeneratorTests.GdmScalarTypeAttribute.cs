using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.OGraph.CodeAnalysis.Tests;

public class GdmScalarTypeAttributeSourceGeneratorTests : SourceGeneratorTestBase
{

    [Fact]
    public void ClassScalarTypeGeneratorTest()
    {
        // Arrange
        var source = @"
using Assimalign.OGraph.Gdm;

namespace ErpCore;

[GdmScalarType(ScalarUnderlyingType.Guid)]
public partial class UserId
{
    
}";

        var result = RunGenerator<GdmScalarTypeAttributeIncrementalGenerator>(
            source,
            "EmbeddedResources.GdmScalarTypeAttribute.cs");

        // Assert No diagnostics
        Assert.True(result.Diagnostics.IsEmpty);

        // Assert there are two trees. One for the Runtime Type and the other for the GdmType
        Assert.True(result.GeneratedTrees.Length == 2);

        // You can also verify the exact generated source if needed
        var generatedSource = result.GeneratedTrees[0].ToString();
        Assert.NotNull(generatedSource);
    }
}
