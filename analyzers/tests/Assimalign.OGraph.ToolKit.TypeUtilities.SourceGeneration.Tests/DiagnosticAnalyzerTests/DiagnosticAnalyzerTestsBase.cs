using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.ToolKit.Analyzers.Tests;

public abstract class DiagnosticAnalyzerTestsBase
{

    protected async Task VerifyAsync<TAnalyzer>() where TAnalyzer : DiagnosticAnalyzer, new()
    {
        var context = new CSharpAnalyzerTest<TAnalyzer, DefaultVerifier>();
        context.ReferenceAssemblies = ReferenceAssemblies.Net.Net90;
        context.TestCode = """
            [ValueType(UnderlyingType.String, IncludeIsValidMethod = true)]
            [PickType("Test")]
            public partial class EmployeeId
            {


            }
            """;

        await context.RunAsync();
    }
}
