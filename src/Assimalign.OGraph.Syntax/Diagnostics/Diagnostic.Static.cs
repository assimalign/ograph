using Assimalign.OGraph.Syntax.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed partial class Diagnostic
{

    internal static Diagnostic UnexpectedEOF(int end)
    {
        return new Diagnostic()
        {
            Code = DiagnosticCode.G0000.ToString(),
            Message = "Unexpected EOF (End-Of-File).",
            Start = end,
            End = end,
            Location = DiagnosticLocation.Relative,
            Severity = DiagnosticSeverity.Error,
        };
    }
    internal static Diagnostic ExpectedOpeningParenthesis(int start, int end)
    {
        return new Diagnostic()
        {
            Code = DiagnosticCode.G0001.ToString(),
            Message = "Expected: '('",
            Start = start,
            End = end,
            Location = DiagnosticLocation.Absolute,
            Severity = DiagnosticSeverity.Error,
        };
    }
    internal static Diagnostic ExpectedClosingParenthesis(int start, int end)
    {
        return new Diagnostic()
        {
            Code = DiagnosticCode.G0002.ToString(),
            Message = "Expected: ')'",
            Start = start,
            End = end,
            Location = DiagnosticLocation.Absolute,
            Severity = DiagnosticSeverity.Error,
        };
    }
    internal static Diagnostic ExpectedOpeningBracket(int start, int end)
    {
        return new Diagnostic()
        {
            Code = DiagnosticCode.G0003.ToString(),
            Message = "Expected: '{'",
            Start = start,
            End = end,
            Location = DiagnosticLocation.Absolute,
            Severity = DiagnosticSeverity.Error,
        };
    }
    internal static Diagnostic ExpectedClosingBracket(int start, int end)
    {
        return new Diagnostic()
        {
            Code = DiagnosticCode.G0004.ToString(),
            Message = "Expected: '}'",
            Start = start,
            End = end,
            Location = DiagnosticLocation.Absolute,
            Severity = DiagnosticSeverity.Error,
        };
    }
    internal static Diagnostic ExpectedCommaSeparator(int start, int end)
    {
        return new Diagnostic()
        {
            Code = DiagnosticCode.G0005.ToString(),
            Message = "Expected: ','",
            Start = start,
            End = end,
            Location = DiagnosticLocation.Relative,
            Severity = DiagnosticSeverity.Error,
        };
    }
    internal static Diagnostic ExpectedDotSeparator(int start, int end)
    {
        return new Diagnostic()
        {
            Code = DiagnosticCode.G0005.ToString(),
            Message = "Expected: '.'",
            Start = start,
            End = end,
            Location = DiagnosticLocation.Relative,
            Severity = DiagnosticSeverity.Error,
        };
    }

    internal static Diagnostic InvalidToken(ref Token token)
    {
        return new Diagnostic()
        {
            Code = DiagnosticCode.G0007.ToString(),
            Message = $"Invalid Token: {token}",
            Start = token.Start,
            End = token.End,
            Location = DiagnosticLocation.Relative,
            Severity = DiagnosticSeverity.Error,
        };
    }
}
