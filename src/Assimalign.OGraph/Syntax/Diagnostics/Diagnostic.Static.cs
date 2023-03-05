using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed partial class Diagnostic
{


    internal static Diagnostic GetUnexpectedEOFError(int start, DiagnosticLocation location = DiagnosticLocation.Relative)
    {
        return new Diagnostic()
        {
            Code = DiagnosticCode.G0005,
            Message = $"Unexpected EOF (end-of-file) at '{start}'.",
            Start = start,
            End = start,
            Location = location,
            Severity = DiagnosticSeverity.Error,
        };
    }
}
