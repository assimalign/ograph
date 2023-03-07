using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal class IdentifierParser : Parser
{
    

    /* Should return either a:
     * - property node 
     * - function node 
     
     */
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        var token = lexer.Current;

        if (lexer.Current.TokenType != TokenType.Identifier)
        {
            // TODO: Add diagnostics 
            return queryNode;
        }
        if (queryNode is FunctionQueryNode functionNode)
        {
            queryNode = new FunctionQueryNode()
            {

            };
            if (token.Value.IsFunction(out var functionType))
            {

            }
            else
            {

            }
                queryNode = new Function
        }

        

        if (token.Value.IsFunction(out var functionType)) 
        {
             
        }
        else
        {
            return ParseProperty(ref lexer, context, queryNode);
        }



        QueryNode ParseFunction(FunctionType functionType)
        {
            return functionType switch
            {
                FunctionType.SubString => ParseSubStringFunctionNode(ref lexer, context, new FunctionQueryNode())
            };
        }
    }




    private QueryNode ParseProperty(ref TokenLexer lexer, ParserContext context, QueryNode fieldNode)
    {
        return new PropertyQueryNode(default);
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
    private QueryNode ParseSubStringFunctionNode(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {
        return default;
    }
}
