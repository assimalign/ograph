using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal class FilterParser : Parser<FilterNode>
{
    internal override FilterNode Parse(ref TokenLexer lexer, ParserContext context, FilterNode queryNode)
    {
        Token token;

        // Ensure not EOF (End of File)
        if (!lexer.HasNext)
        {
            AddEofDiagnostic(ref lexer, context);
            return queryNode;
        }

        token = lexer.Next();

        // Ensure next token is an Open Parenthesis Block
        if (token.TokenType != TokenType.OpenParenthesis)
        {
            AddExpectedOpenParenDiagnostic(ref lexer, context);
            return queryNode;
        }

        return ParseParenthesisBlock(ref lexer, context, queryNode);
    }
    private FilterNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context, FilterNode queryNode)
    {
        Token token;

        // Ensure not EOF (End of File)
        if (!lexer.HasNext)
        {
            AddEofDiagnostic(ref lexer, context);
            return queryNode;
        }

        token = lexer.Next();

        // Ensure next token is bracket block
        if (token.TokenType == TokenType.OpenBracket)
        {
            AddExpectedOpenBracketDiagnostic(ref lexer, context);
            return queryNode;
        }

        // Parse Parenthesis Block
        while (lexer.HasNext)
        {
            token = lexer.Next();

            if (token.TokenType == TokenType.CloseParenthesis)
            { 
                // If there is more token after the closing parenthesis and no dot separator, then error
                if (lexer.TryPeek(out var peek) && peek.TokenType != TokenType.Dot)
                {
                    lexer.Next();
                    AddExpectedDotSeparatorDiagnostic(ref lexer, context);
                }

                return queryNode;
            }

            queryNode = ParseBracketBlock(ref lexer, context, queryNode);
        }

        AddExpectedClosingParenDiagnostic(ref lexer, context);

        return queryNode;
    }
    private FilterNode ParseBracketBlock(ref TokenLexer lexer, ParserContext context, FilterNode queryNode)
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
                        leftOperand = context.GetParser<FunctionParser>()
                            .Parse(ref lexer, context, new FunctionCallNode()
                        {
                            FunctionType = functionType
                        });
                    }
                    break;
                case TokenType.Identifier:
                    {
                        leftOperand = context.GetParser<PropertyParser>()
                            .Parse(ref lexer, context, new PropertyNode());
                        break;
                    }
                case TokenType.Null:
                case TokenType.String:
                case TokenType.Integer:
                case TokenType.FloatingPoint:
                case TokenType.Boolean:
                    {
                        leftOperand = context.GetParser<ConstantParser>()
                            .Parse(ref lexer, context, new ConstantNode());
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

                        if (parser.Parse(ref lexer, context, new BinaryNode() {  LeftOperand = leftOperand }) is not BinaryNode binaryNode1)
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

        if (leftOperand is not BinaryNode binaryNode)
        {
            // TODO: 
            return queryNode;
        }

        queryNode = new FilterNode()
        {
            //Identifier = queryNode.Identifier,
            Predicate = binaryNode
        };

        return queryNode;
    }
}
