using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public sealed partial class QueryParser
{
    private readonly QueryParserOptions options;

    public QueryParser()
    {
        options = QueryParserOptions.Default;
    }

    public QueryParser(QueryParserOptions options)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }
        this.options = options;
    }

    public QueryDocument Parse(string query)
    {
        try
        {
            var buffer = options.Encoding.GetBytes(query);
            var lexer = new TokenLexer(buffer, new()
            {
                SkipCarriageReturn = true,
                SkipLineFeed = true,
                SkipTabs = true,
                SkipWhiteSpace = true,
                SkipComments = true,
                Encoding = options.Encoding
            });
            var context = new ParserContext()
            {
                //Root = options.VertexFactory is null ? RootNode.Create(): new(options.VertexFactory.Invoke()),
                Encoding = options.Encoding,
                ThrowExceptionOnDiagnosticError = options.ThrowExceptionOnDiagnosticError
            };
            // NOTE: The Parser is responsible for only syntax diagnostics
            //       Analyzers will be responsible for semantic diagnostics.
            var node = ParseRoot(ref lexer, context);
            var document = new QueryDocument(
                query,
                node!,
                context.Diagnostics);

            Analyze(document, options.AnalyzerTimeout);

            return document;
        }
        catch (OperationCanceledException exception)
        {
            throw;
        }
        catch (TokenLexerException exception)
        {
            throw new QueryParserException(exception);
        }
    }

    private void Analyze(QueryDocument document, TimeSpan timeout)
    {
        using var cancellationTokenSource = new CancellationTokenSource(timeout); // Max 10 seconds for analysis
#if !DEBUG
        cancellationTokenSource.Token.ThrowIfCancellationRequested();
#endif
        var analyzers = new List<Task>();

        foreach (var analyzer in options.Analyzers)
        {
            analyzers.Add(analyzer.AnalyzeAsync(document, cancellationTokenSource.Token));
        }
        while (analyzers.Any())
        {
            var task = Task.WhenAny(analyzers);

            while (!task.IsCompleted)
            {
                if (cancellationTokenSource.IsCancellationRequested)
                {
                    throw new OperationCanceledException(cancellationTokenSource.Token);
                }
            }

            analyzers.Remove(task.Result);
        }
    }


    #region Helper Methods
    private void SkipToClosingBracket(ref TokenLexer lexer)
    {
        Token token;

        while (lexer.TryNext(out token))
        {
            if (token.TokenType == TokenType.OpenBracket)
            {
                break;
            }
        }
    }
    private void SkipToClosingParenthesis(ref TokenLexer lexer)
    {
        Token token;

        while (lexer.TryNext(out token))
        {
            if (token.TokenType == TokenType.OpenBracket)
            {
                break;
            }
        }
    }
    #endregion

    private void AddInvalidTokenDiagnostic(ref TokenLexer lexer, ParserContext context)
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

    private void AddEofDiagnostic(ref TokenLexer lexer, ParserContext context)
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
    private void AddExpectedOpenParenDiagnostic(ref TokenLexer lexer, ParserContext context)
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
    private void AddExpectedOpenBracketDiagnostic(ref TokenLexer lexer, ParserContext context)
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
    private void AddExpectedDotSeparatorDiagnostic(ref TokenLexer lexer, ParserContext context)
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
    private void AddExpectedClosingParenDiagnostic(ref TokenLexer lexer, ParserContext context)
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

    private void AddExpectedIntegerDiagnostic(ref TokenLexer lexer, ParserContext context)
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


    //protected void AddDuplicateKeywordDiagnostic(ref TokenLexer lexer, ParserContext context)
    //{

    //}
}