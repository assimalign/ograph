using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal class FunctionParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        if (queryNode is not FunctionCallNode functionNode)
        {
            throw QueryParserException.UnexpectedQueryNode(
                typeof(FunctionCallNode),
                queryNode.GetType());
        }
        if (!lexer.Current.Value.IsFunction(out var functionType))
        {
            // TODO: Expected Function Type
            return queryNode;
        }

        return functionType switch
        {
            FunctionType.StartsWith => ParseFunctionStartsWtih(ref lexer, context, functionNode),
            FunctionType.SubString => ParseSubStringFunctionNode(ref lexer, context, functionNode),
            _ => throw new Exception()
        };
    }


    private QueryNode ParseFunctionAny(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        return queryNode;
    }

    private QueryNode ParseFunctionEndsWith(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {

        return queryNode;
    }
    private QueryNode ParseFunctionStartsWtih(ref TokenLexer lexer, ParserContext context, FunctionCallNode queryNode)
    {
        var parameters = new Queue<ParameterNode>();

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
                            parameters.Enqueue(new ParameterNode()
                            {
                                ParameterValue = context.GetParser<PropertyParser>().Parse(ref lexer, context, new PropertyNode())
                            });
                        }
                        break;
                    }
                case TokenType.String:
                    {
                        parameters.Enqueue(new ParameterNode()
                        {
                            ParameterValue = context.GetParser<ConstantParser>().Parse(ref lexer, context, new ConstantNode())
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

        queryNode = new FunctionCallNode()
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

        if (parser.Parse(ref lexer, context, new ParameterNode()) is not ParameterNode parameter)
        {
            // TODO: Add diagnostic
            return queryNode;
        }
        if (lexer.TryNext(out next) && next.TokenType != TokenType.Comma)
        {
            // TODO: Expected comma separator
        }
        return queryNode;
    }
}
