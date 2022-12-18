using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

[System.Diagnostics.DebuggerDisplay("{Severity}: ({Start}..{Start+Length}): {Message}")]
public sealed partial class Diagnostic
{
    public Diagnostic() { }
    public Diagnostic(DiagnosticCode code, string? message, int start, int end, DiagnosticSeverity severity)
    {
        Code = code;
        Message = message;
        Start = start;
        End = end;
        Severity = severity;
    }


    /// <summary>
    /// The diagnostic code.
    /// </summary>
    public DiagnosticCode Code { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public DiagnosticLocation Location { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public string? Message { get; init; }
    /// <summary>
    /// The length of the error.
    /// </summary>
    public int Length => End - Start;
    /// <summary>
    /// Start of diagnostic location in the source.
    /// </summary>
    public int Start { get; init; }
    /// <summary>
    /// End of diagnostic location in the source.
    /// </summary>
    public int End { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public DiagnosticSeverity Severity { get; init; }
}
