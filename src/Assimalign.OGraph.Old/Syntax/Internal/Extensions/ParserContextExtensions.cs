using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal static class ParserContextExtensions
{

    internal static void AddUnexptedTokenError(this ParserContext context, ref TokenLexer lexer)
    {
        context.AddDiagnostic(new Diagnostic()
        {
            Severity = DiagnosticSeverity.Error,
            Location = DiagnosticLocation.Relative,
            Start = lexer.Current.Start,
            End = lexer.Current.End,
            Message = $"Unexpected Token: {lexer.Current}"
        });
    }
    internal static void AddUnexpectedEOFError(this ParserContext context, ref TokenLexer lexer)
    {
        context.AddDiagnostic(new Diagnostic()
        {
            Code = DiagnosticCode.G0005.ToString(),
            Message = $"Unexpected EOF (end-of-file) at '{lexer.Current.End}'.",
            Start = lexer.Current.End,
            End = lexer.Current.End,
            Location = DiagnosticLocation.Relative,
            Severity = DiagnosticSeverity.Error,
        });
    }


    internal static void AddExpectedParenthesisDiagnosticError(this ParserContext context, int end)
    {

    }

    internal static void AddExpectedCommaSeparatorDiagnosticError(this ParserContext context, ref Token lexerToken)
    {
        context.AddDiagnostic(new Diagnostic()
        {
            Severity = DiagnosticSeverity.Error,
            Location = DiagnosticLocation.Absolute,
            Start = lexerToken.Start,
            End = lexerToken.End,
            Message = $"Expected Comma"
        });
    }
}
