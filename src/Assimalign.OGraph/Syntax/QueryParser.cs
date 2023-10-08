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
                Root = new VertexNode(),
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
                context.Diasgnostics);

            var analyzers = GetAnalyzers(document, CancellationToken.None);

            while (analyzers.Any())
            {
                var task = Task.WhenAny(analyzers);
                task.Wait();
                analyzers.Remove(task.Result);
            }

            return document;
        }
        catch (TokenLexerException exception)
        {
            throw new QueryParserException(exception);
        }
    }


    private IList<Task> GetAnalyzers(QueryDocument document, CancellationToken cancellationToken)
    {
        var list = new List<Task>();

        foreach (var analyzer in options.Analyzers)
        {
            list.Add(analyzer.AnalyzeAsync(document, cancellationToken));
        }

        return list;
    }
}