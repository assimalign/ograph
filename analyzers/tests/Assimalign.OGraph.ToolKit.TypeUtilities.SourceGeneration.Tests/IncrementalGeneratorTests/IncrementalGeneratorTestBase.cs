using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.ToolKit.TypeUtilities.SourceGeneration.Tests;

public abstract class IncrementalGeneratorTestBase
{
    protected static GeneratorDriverRunResult RunGenerator<TGenerator>(string sourceCode, string sourceAttrResource) where TGenerator : IIncrementalGenerator, new()
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
    protected static Compilation Compile(string source, string attributeSource)
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
            nameof(Assimalign.OGraph.ToolKit.TypeUtilities.SourceGeneration.Tests),
            syntaxTrees,
            references,
            compilationOptions);
    }
    protected static string GetSource(string embeddedResourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var assemblyName = assembly.GetName();

        using var stream = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream($"{assemblyName.Name}.{embeddedResourceName}")!;

        using var streamReader = new StreamReader(stream, Encoding.UTF8);

        return streamReader.ReadToEnd();
    }
}
