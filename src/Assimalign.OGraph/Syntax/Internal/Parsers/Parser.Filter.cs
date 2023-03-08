using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal class FilterParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        if (queryNode is not FilterQueryNode filterNode)
        {
            // This is internal error. Some dumbass messed with the code.
            return queryNode;
        }
        if (!lexer.HasNext)
        {
            context.AddUnexpectedEOFError(ref lexer);
            return queryNode;
        }

        var token = lexer.Next();

        if (token.TokenType != TokenType.OpenParenthesis)
        {
            // TODO: Add diagnostic error. Expected starting parenthesis block
            return queryNode;
        }

        return ParseParenthesisBlock(ref lexer, context, filterNode);
    }
    private FilterQueryNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context, FilterQueryNode queryNode)
    {
        var next = default(Token);

        if (!lexer.TryPeek(out next))
        {
            // TODO: Add Diagnostic error. Unexpected EOF
            return queryNode;
        }
        // Check if projection is followed by an edge identifier
        if (next.TokenType == TokenType.Identifier)
        {
            var edgeParser = context.GetParser<EdgeParser>();
            var edgeNode = edgeParser.Parse<EdgeQueryNode>(ref lexer, context);

            queryNode = new FilterQueryNode()
            {
                Edge = edgeNode
            };

            if (!lexer.TryPeek(out next))
            {
                // TODO: Add Diagnostic error. Unexpected EOF
                return queryNode;
            }
        }
        if (next.TokenType != TokenType.OpenBracket)
        {
            // TODO: Add diagnostic error. Expected starting bracket block
            return queryNode;
        }
        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.CloseParenthesis)
            {
                if (lexer.TryPeek(out var peek) && peek.TokenType != TokenType.Dot)
                {
                    // TODO: Diagnostics error dot notation is required
                }

                return queryNode;
            }

            queryNode = ParseBracketBlock(ref lexer, context, queryNode);
        }

        // TODO: Add diagnostics error. Missing Closing Parentheisis


        return queryNode;
    }
    private FilterQueryNode ParseBracketBlock(ref TokenLexer lexer, ParserContext context, FilterQueryNode queryNode)
    {
        var leftOperand = default(QueryNode);

        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.CloseBracket)
            {
                break;
            }
            switch (token.TokenType)
            {
                case TokenType.Identifier when token.Value.IsFunction(out var functionType):
                    {
                        leftOperand = context.GetParser<FunctionParser>().Parse(ref lexer, context, new FunctionQueryNode()
                        {
                            FunctionType = functionType
                        });
                    }
                    break;
                case TokenType.Identifier:
                    {
                        break;
                    }
                case TokenType.String:
                case TokenType.Integer:
                case TokenType.FloatingPoint:
                case TokenType.Boolean:
                    {
                        leftOperand = context.GetParser<ConstantParser>().Parse(ref lexer, context, queryNode);
                        break;
                    }
                case TokenType.Equal:
                case TokenType.NotEqual:
                case TokenType.GreaterThan:
                case TokenType.GreaterThanOrEqual:
                case TokenType.LessThan:
                case TokenType.LessThanOrEqual:
                case TokenType.And:
                case TokenType.Or:
                    {
                        var parser = context.GetParser<BinaryParser>();
                        if (parser.Parse(ref lexer, context, new BinaryQueryNode() {  LeftOperand = leftOperand }) is not BinaryQueryNode binaryNode1)
                        {
                            // TODO: Add diagnostic
                            continue;
                        }
                        leftOperand = binaryNode1;
                        break;
                    }
                default:
                    {
                        // TODO: Add Diagnostic information. Unexpected next
                        break;
                    }
            }
        }

        if (leftOperand is not BinaryQueryNode binaryNode)
        {
            // TODO: 
            return queryNode;
        }

        queryNode = new FilterQueryNode()
        {
            Edge = queryNode.Edge,
            Predicate = binaryNode
        };

        return queryNode;
    }
}
