using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Xunit;
using System.Reflection;
using System.Text;
using System.CodeDom;
using System.Xml.Linq;

namespace Assimalign.OGraph.CodeAnalysis.Tests;


public class CodeGenTests
{

    [Fact]
    public void TestScalarGenerator()
    {
        // Arrange
        var source = @"

using Assimalign.OGraph.Gdm;

namespace ErpCore;

[GdmScalarType(ScalarUnderlyingType.Int, 
    IncludeImplicitOperators = true)]
public partial class UserId
{
    
}";

        var result = RunGenerator<GdmScalarTypeAttributeIncrementalGenerator>(
            source,
            "EmbeddedResources.GdmScalarTypeAttribute.cs");

        // Assert
        Assert.True(result.Diagnostics.IsEmpty);
        Assert.True(result.GeneratedTrees.Length > 0);

        // You can also verify the exact generated source if needed
        var generatedSource = result.GeneratedTrees[0].ToString();
        Assert.NotNull(generatedSource);

    }


    private static GeneratorDriverRunResult RunGenerator<TGenerator>(string sourceCode, string sourceAttrResource) where TGenerator : IIncrementalGenerator, new()
    {
        string? sourceAttrCode = GetSource(sourceAttrResource);

        Assert.NotNull(sourceCode);
        Assert.NotNull(sourceAttrCode);

        IIncrementalGenerator generator = new TGenerator();

        Compilation compilation = Compile(sourceCode, sourceAttrCode);

        // Run generator
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator).RunGenerators(compilation);


        driver = driver.RunGenerators(compilation);

        // Use Microsoft.CodeAnalysis.Testing verification
        return driver.GetRunResult();
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
            nameof(Assimalign.OGraph.CodeAnalysis.Tests),
            syntaxTrees,
            references,
            compilationOptions);
    }
    private static string GetSource(string embeddedResourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var assemblyName = assembly.GetName();

        using var stream = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream($"{assemblyName.Name}.{embeddedResourceName}")!;

        using var streamReader = new StreamReader(stream, Encoding.UTF8);

        return streamReader.ReadToEnd();
    }



    [Fact]
    public void Test1()
    {
        // Arrange
        var source = @"
namespace TestNamespace
{
    [Assimalign.OGraph.ScalarType(ScalarUnderlyingType.Int, IncludeImplicitOperators = true)]
    public partial struct TestStruct
    {
    }
}";

        var attributeSource = GetSource("CodeGenAttributes.Attribute.GdmScalarType.cs");

        Assert.NotNull(source);

        // Create compilation
        var compilation = Compile(source, attributeSource);

        var generator = new GdmScalarTypeAttributeIncrementalGenerator();

        // Run generator
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator).RunGenerators(compilation);

        
        driver = driver.RunGenerators(compilation);

        // Use Microsoft.CodeAnalysis.Testing verification
        var result = driver.GetRunResult();

        // Assert
        Assert.True(result.Diagnostics.IsEmpty);
        Assert.True(result.GeneratedTrees.Length > 0);

        // You can also verify the exact generated source if needed
        var generatedSource = result.GeneratedTrees[0].ToString();
        Assert.NotNull(generatedSource);

        //CSharpSourceGeneratorVerifier<ScalarTypeAttributeIncrementalGenerator, Ver>
    }

    [Fact]
    public void Test2()
    {

        var source = @"
namespace TestNamespace
{
    [Assimalign.OGraph.PickObjectType(Name = ""UserCreatedInput"", Properties = [""Name""])]
    public partial class User
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Created { get; set; }
    }
}";
        var attributeSource = @"
namespace Assimalign.OGraph
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class PickObjectTypeAttribute : Attribute
    {
        public PickObjectTypeAttribute()
        {
            
        }

        public string Name { get; set; }
        public string[] Properties { get; set; } = [];
    }
}
";

        // Create compilation
        var compilation = Compile(source, attributeSource);

        var generator = new PickComplexTypeAttributeIncrementalGenerator();

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

    }

    
}
