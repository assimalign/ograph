using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal class FunctionParser : Parser
{


    /* Should return either a:
     * - property node 
     * - function node 
     
     */
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        if (queryNode is not FunctionQueryNode functionNode)
        {
            throw QueryParserException.UnexpectedQueryNode(
                typeof(FunctionQueryNode),
                queryNode.GetType());
        }

        return functionNode.FunctionType switch
        {
            FunctionType.SubString => ParseSubStringFunctionNode(ref lexer, context, new FunctionQueryNode())
        };
    }


    private QueryNode ParseFunctionAny(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {
        return default;
    }

    private QueryNode ParseFunctionEndsWith(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {

        return default;
    }
    private QueryNode ParseFunctionStartsWtih(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {

        return default;
    }
    private QueryNode ParseSubStringFunctionNode(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        var next = default(Token);

        if (lexer.Current.TokenType != TokenType.Identifier)
        {
            // TODO: Add diagnostics
            return queryNode;
        }

        var parser = context.GetParser<ParameterParser>();

        if (parser.Parse(ref lexer, context, new ParameterQueryNode()) is not ParameterQueryNode parameter)
        {
            // TODO: Add diagnostic
            return queryNode;
        }
        if (lexer.TryNext(out next) && next.TokenType != TokenType.Comma)
        {
            // TODO: Expected comma separator
        }
        return default;
    }
}
