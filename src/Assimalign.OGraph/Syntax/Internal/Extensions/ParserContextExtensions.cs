using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal static class ParserContextExtensions
{

    internal static void AddUnexptedTokenDiagnosticError(this ParserContext context,ref Token lexerToken)
    {
        context.AddDiagnostic(new Diagnostic()
        {
            Severity = DiagnosticSeverity.Error,
            Location = DiagnosticLocation.Relative,
            Start = lexerToken.Start,
            End = lexerToken.End,
            Message = $"Unexpected Token: {lexerToken}"
        });
    }
    internal static void AddUnexpectedEOFDiagnosticError(this ParserContext context, int end)
    {
        context.AddDiagnostic(new Diagnostic()
        {
            Code = DiagnosticCode.G0005,
            Message = $"Unexpected EOF (end-of-file) at '{end}'.",
            Start = end,
            End = end,
            Location = DiagnosticLocation.Relative,
            Severity = DiagnosticSeverity.Error,
        });
    }


    internal static void AddExpectedParenthesisDiagnosticError(this ParserContext context, int end)
    {

    }
}
