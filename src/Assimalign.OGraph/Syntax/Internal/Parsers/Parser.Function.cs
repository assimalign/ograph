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
            FunctionType.StartsWith => ParseFunctionStartsWtih(ref lexer, context, functionNode),
            FunctionType.SubString => ParseSubStringFunctionNode(ref lexer, context, functionNode)
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
    private QueryNode ParseFunctionStartsWtih(ref TokenLexer lexer, ParserContext context, FunctionQueryNode queryNode)
    {
        var parameters = new Queue<ParameterQueryNode>();

        if (!lexer.TryPeek(out var peek) || peek.TokenType != TokenType.OpenParenthesis)
        {
            // TODO: Add Diagnostic
            return queryNode;
        }
        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.CloseParenthesis)
            {
                break;
            }
            switch (token.TokenType)
            {
                case TokenType.Identifier:
                    {
                        if (token.Value.IsFunction(out var functionType))
                        {

                        }
                        else
                        {
                            parameters.Enqueue(new ParameterQueryNode()
                            {
                                ParameterValue = context.GetParser<PropertyParser>().Parse<PropertyQueryNode>(ref lexer, context)
                            });
                        }
                        break;
                    }
                case TokenType.String:
                    {
                        parameters.Enqueue(new ParameterQueryNode()
                        {
                            ParameterValue = context.GetParser<ConstantParser>().Parse<ConstantQueryNode>(ref lexer, context)
                        });
                        break;
                    }
                default:
                    {
                        // TODO: Unexpected Token
                        break;
                    }
            }
            if (token.IsIdentifier) 
            {
                
            }
            
            if (!lexer.TryPeek(out var next) || (next.TokenType != TokenType.Comma || next.TokenType != TokenType.CloseParenthesis))
            {
                // TODO: Add Diagnostics
            }
        }
        if (parameters.Count > 2 || parameters.Count < 2)
        {
            // TODO : Add Diagnostics
        }

        queryNode = new FunctionQueryNode()
        {
            FunctionType = queryNode.FunctionType,
            Name = "startswith",
            Parameters = parameters,
        };

        return queryNode;
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
