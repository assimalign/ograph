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

    private string GetSource(string embeddedResourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var assemblyName = assembly.GetName();

        // For debugging
        // var manifestResourceNames = assembly.GetManifestResourceNames();

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
        var source = GetSource("CodeGenAttributes.Attribute.GdmScalarType.cs");
        var attributeSource = @"
namespace Assimalign.OGraph
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
    public sealed class ScalarTypeAttribute : Attribute
    {
        public ScalarTypeAttribute(ScalarUnderlyingType underlyingType)
        {

        }

        /// <summary>
        /// The underlying runtime type to use for the entity key.
        /// </summary>
        public ScalarUnderlyingType UnderlyingType { get; }

        /// <summary>
        /// Specifies whether to include an implicit operators that convert to and from 
        /// the underlying runtime type.
        /// </summary>
        public bool IncludeImplicitOperators { get; set; }

        /// <summary>
        /// Write a partial bool method for 
        /// </summary>
        public bool IncludeIsValidMethod { get; set; }
    }

    public enum ScalarUnderlyingType
    {
        Int,
        Short,
        Long,
        UInt,
        UShort,
        ULong,
        String,
        Guid
    }
}";

        // Create compilation
        var compilation = Compile(source, attributeSource);

        var generator = new ScalarTypeAttributeIncrementalGenerator();

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
