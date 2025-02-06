using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Xunit;

namespace Assimalign.OGraph.CodeAnalysis.Tests;


public class UnitTest1
{
    [Fact]
    public void Test1()
    {

        // Arrange
        var source = @"
namespace TestNamespace
{
    [Assimalign.OGraph.EntityKey(EntityKeyKind.Guid, IncludeImplicitOperators = true)]
    public partial struct TestStruct
    {
    }
}";

        var attributeSource = @"
namespace Assimalign.OGraph
{
    [AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
    public sealed class EntityKeyAttribute : Attribute
    {

        public EntityKeyAttribute(EntityKeyKind kind = EntityKeyKind.Int)
        {
            Kind = kind;
        }
        public EntityKeyKind Kind { get; }
        public bool IncludeImplicitOperators { get; set; }
    }

    public enum EntityKeyKind
    {
        String,
        Int,
        Short,
        Long,
        UInt,
        UShort,
        ULong,
        Guid
    }
}";

        // Create compilation
        var compilation = Compile(source, attributeSource);

        var generator = new EntityKeyAttributeIncrementalGenerator();

        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        // Run generator
        driver = driver.RunGenerators(compilation);

        // Use Microsoft.CodeAnalysis.Testing verification
        var result = driver.GetRunResult();

        // Assert
        Assert.True(result.Diagnostics.IsEmpty);
        Assert.True(result.GeneratedTrees.Length > 0);

        // You can also verify the exact generated source if needed
        var generatedSource = result.GeneratedTrees[0].ToString();
        Assert.NotNull(generatedSource);

        CSharpSourceGeneratorVerifier<EntityKeyAttributeIncrementalGenerator, Ver>
    }



    private static Compilation Compile(string source, string attributeSource)
    {
        var syntaxTrees = new[]
        {
            CSharpSyntaxTree.ParseText(source),
            CSharpSyntaxTree.ParseText(attributeSource)
        };

        var references = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute).Assembly.Location)
        };

        var compilationOptions = new CSharpCompilationOptions(
            OutputKind.DynamicallyLinkedLibrary);

        return CSharpCompilation.Create(
            "test",
            syntaxTrees,
            references,
            compilationOptions);
    }
}
