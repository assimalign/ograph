using Assimalign.OGraph.ToolKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.OGraph.ToolKit.Tests;

public class ValueTypeAttributeSourceGeneratorTests : IncrementalGeneratorTestBase
{

    [Fact]
    public void ClassValueTypeGeneratorTest()
    {
        // Arrange
        var source = @"
using System;

namespace ErpCore;

[ValueType(ValueUnderlyingType.String, IncludeIsValidMethod = true)]
public partial class UserId
{
    
}";

        var result = RunGenerator<ValueTypeIncrementalGenerator>(
            source,
            "EmbeddedResources.Attribute.ValueType.cs");

        // Assert No diagnostics
        Assert.True(result.Diagnostics.IsEmpty);

        // Assert there are two trees. One for the Runtime Type and the other for the GdmType
        Assert.True(result.GeneratedTrees.Length == 2);

        // You can also verify the exact generated source if needed
        var generatedSource = result.GeneratedTrees[0].ToString();
        Assert.NotNull(generatedSource);
    }
}
