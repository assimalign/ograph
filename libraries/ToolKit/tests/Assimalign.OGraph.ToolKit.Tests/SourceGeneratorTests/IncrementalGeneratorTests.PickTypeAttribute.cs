using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.ToolKit.Tests;


public class PickTypeAttributeSourceGeneratorTests : IncrementalGeneratorTestBase
{

    [Fact]
    public void PickTypeGeneratorTest()
    {
        // Arrange
        var source = @"
using System;

namespace ErpCore;

[PickType(""EmployeeCreateInput"", Properties = [ ""Name"", ""Email"", nameof(Value) ])]
public partial class Employee
{
    private int _id;

    public int Id { get => _id; set =>_id = value; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Test? Value { get; set; }
}


public struct Test
{
    public int Test { get; set; }
}
";

        var result = RunGenerator<PickTypeIncrementalGenerator>(
            source,
            "EmbeddedResources.Attribute.PickType.cs");

        // Assert No diagnostics
        Assert.True(result.Diagnostics.IsEmpty);

        // Assert there are two trees. One for the Runtime Type and the other for the GdmType
        Assert.True(result.GeneratedTrees.Length == 2);

        // You can also verify the exact generated source if needed
        var generatedSource = result.GeneratedTrees[0].ToString();
        Assert.NotNull(generatedSource);
    }
}
