using System;

namespace Assimalign.OGraph.Syntax.Internal;

internal abstract class Parser
{
    internal abstract QueryNode Parse(ref TokenLexer lexer, ParserContext context);
    //internal abstract QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode);


    protected void AddInvalidTokenDiagnostic(ref TokenLexer lexer, ParserContext context)
    {
        context.AddDiagnostic(new Diagnostic()
        {
            Code = DiagnosticCode.G0006.ToString(),
            Message = "Invalid Token.",
            Start = lexer.Current.Start,
            End = lexer.Current.End,
            Line = lexer.Current.Line,
            Location = DiagnosticLocation.Relative,
            Severity = DiagnosticSeverity.Error,
        });
    }

    protected void AddEofDiagnostic(ref TokenLexer lexer, ParserContext context)
    {
        context.AddDiagnostic(new Diagnostic()
        {
            Code = DiagnosticCode.G0000.ToString(),
            Message = "Unexpected EOF (End-Of-File).",
            Start = lexer.Current.Start,
            End = lexer.Current.End,
            Line = lexer.Current.Line,
            Location = DiagnosticLocation.Relative,
            Severity = DiagnosticSeverity.Error,
        });
    }
    protected void AddExpectedOpenParenDiagnostic(ref TokenLexer lexer, ParserContext context)
    {
        context.AddDiagnostic(new Diagnostic()
        {
            Code = DiagnosticCode.G0001.ToString(),
            Message = "Expected: '('",
            Start = lexer.Current.Start,
            End = lexer.Current.End,
            Line = lexer.Current.Line,
            Location = DiagnosticLocation.Absolute,
            Severity = DiagnosticSeverity.Error,
        });
    }
    protected void AddExpectedOpenBracketDiagnostic(ref TokenLexer lexer, ParserContext context)
    {
        context.AddDiagnostic(new Diagnostic()
        {
            Code = DiagnosticCode.G0003.ToString(),
            Message = "Expected: '{'",
            Start = lexer.Current.Start,
            End = lexer.Current.End,
            Line = lexer.Current.Line,
            Location = DiagnosticLocation.Absolute,
            Severity = DiagnosticSeverity.Error,
        });
    }
    protected void AddExpectedDotSeparatorDiagnostic(ref TokenLexer lexer, ParserContext context)
    {
        context.AddDiagnostic(new Diagnostic()
        {
            Code = DiagnosticCode.G0005.ToString(),
            Message = "Expected: '.'.",
            Start = lexer.Current.Start,
            End = lexer.Current.End,
            Line = lexer.Current.Line,
            Location = DiagnosticLocation.Relative,
            Severity = DiagnosticSeverity.Error,
        });
    }
    protected void AddExpectedClosingParenDiagnostic(ref TokenLexer lexer, ParserContext context)
    {
        context.AddDiagnostic(new Diagnostic()
        {
            Code = DiagnosticCode.G0002.ToString(),
            Message = "Expected: ')'.",
            Start = lexer.Current.Start,
            End = lexer.Current.End,
            Line = lexer.Current.Line,
            Location = DiagnosticLocation.Absolute,
            Severity = DiagnosticSeverity.Error,
        });
    }

    protected void AddExpectedIntegerDiagnostic(ref TokenLexer lexer, ParserContext context)
    {
        context.AddDiagnostic(new Diagnostic()
        {
            Code = DiagnosticCode.G0008.ToString(),
            Message = "Expected Integer.",
            Start = lexer.Current.Start,
            End = lexer.Current.End,
            Line = lexer.Current.Line,
            Location = DiagnosticLocation.Absolute,
            Severity = DiagnosticSeverity.Error,
        });
    }


    protected void AddDuplicateKeywordDiagnostic(ref TokenLexer lexer, ParserContext context)
    {

    }
    


    internal static Parser Create() => new RootParser();
}
