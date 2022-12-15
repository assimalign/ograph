using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed class QueryDiagnostic
{
    private QueryDiagnostic(string code, string? message, int start, int end, QueryDiagnosticSeverity severity)
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
    public string Code { get; }
    /// <summary>
    /// 
    /// </summary>
    public string? Message { get; }
    /// <summary>
    /// 
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
