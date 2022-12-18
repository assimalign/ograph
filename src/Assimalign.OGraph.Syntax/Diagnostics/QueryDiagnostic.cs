using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

[System.Diagnostics.DebuggerDisplay("{Severity}: ({Start}..{Start+Length}): {Message}")]
public sealed class QueryDiagnostic
{
    public QueryDiagnostic(QueryDiagnosticCode code, string? message, int start, int end, QueryDiagnosticSeverity severity)
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
    public QueryDiagnosticCode Code { get; }
    /// <summary>
    /// 
    /// </summary>
    public string? Message { get; }
    /// <summary>
    /// The length of the error.
    /// </summary>
    public int Length => End - Start;
    /// <summary>
    /// Start of diagnostic location in the source.
    /// </summary>
    public int Start { get; }
    /// <summary>
    /// End of diagnostic location in the source.
    /// </summary>
    public int End { get; }
    /// <summary>
    /// 
    /// </summary>
    public QueryDiagnosticSeverity Severity { get; }
}
