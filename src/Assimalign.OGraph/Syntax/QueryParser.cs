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
    
    public QueryParser() : this(new QueryParserOptions()) { }
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
            var buffer  = options.Encoding.GetBytes(query);
            var lexer   = new TokenLexer(buffer, new()
            {
                SkipCarriageReturn  = true,
                SkipLineFeed        = true,
                SkipTabs            = true,
                SkipWhiteSpace      = true,
                SkipComments        = true,
                Encoding            = options.Encoding
            });
            var context = new ParserContext()
            {
                Root = new VertexNode()
                {
                    Label = new LabelNode(options.StartingVertexName!),
                    IsRoot = true,
                },
                Encoding = options.Encoding,
                ThrowExceptionOnDiagnosticError = options.ThrowExceptionOnDiagnosticError
            };
            // NOTE: The Parser is responsible for only syntax diagnostics
            //       Analyzers will be responsible for semantic diagnostics.
            var parser      = Parser.Create();
            var node        = parser.Parse(ref lexer, context, context.Root);
            var document    = new QueryDocument(
                query,
                node,
                context.Diagnostics);

            Analyze(document);

            return document;
        }
        catch (TokenLexerException exception)
        {
            throw new QueryParserException(exception);
        }
    }


    private void Analyze(QueryDocument document)
    {
        using var cancellationTokenSource = new CancellationTokenSource(10000); // Max 10 seconds for analysis
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
            if (!task.IsCompleted) // NOTE: Not sure if this is needed
            {
                task.Wait();
            }
            analyzers.Remove(task.Result);
        }
    }
}