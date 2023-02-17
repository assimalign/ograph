using System;
using System.Text;
using System.Collections.Generic;
//using System.Runtime.CompilerServices;

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


    public QueryDocument Parse(string query) => Parse(options.Encoding.GetBytes(query));    
    private QueryDocument Parse(byte[] query)
    {
        try
        {
            var lexer = new TokenLexer(query, new()
            {
                SkipCarriageReturn = true,
                SkipLineFeed = true,
                SkipTabs = true,
                SkipWhiteSpace = true,
                SkipComments = true,
                Encoding = options.Encoding
            });
            var context = new ParserContext();
            var parser  = Parser.Create();
            var node    = parser.Parse(ref lexer, context, new RootQueryNode());

            foreach (var visitor in options.Visitors)
            {
                visitor.Invoke(node);
            }

            return new QueryDocument(
                node,
                context.Diasgnostics);
        }
        catch (TokenLexerException exception)
        {
            throw new QueryParserException(exception);
        }
    }
}