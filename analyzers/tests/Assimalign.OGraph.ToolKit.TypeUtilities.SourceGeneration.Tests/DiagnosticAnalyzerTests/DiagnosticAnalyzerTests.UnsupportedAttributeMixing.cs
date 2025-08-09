using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.ToolKit.Analyzers.Tests;

public class UnsupportedAttributeMixingDiagnosticAnalyzerTests : DiagnosticAnalyzerTestsBase
{

    [Fact]
    public async Task Temp()
    {
        await VerifyAsync<UnsupportedAttributeMixingDiagnosticAnalyzer>();
    }
}
