using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal class BinaryParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        if (queryNode is not BinaryQueryNode binaryNode)
        {
            // TODO: Add diagnostics
            return queryNode;
        }
        if (binaryNode.LeftOperand is null)
        {
            // TODO: Add diagnostic (A left operand must be supplied)
            return queryNode;
        }
        switch (lexer.Current.TokenType)
        {
            case TokenType.NotEqual:
            case TokenType.Equal:
            case TokenType.GreaterThan:
            case TokenType.GreaterThanOrEqual:
            case TokenType.LessThan:            
            case TokenType.LessThanOrEqual:
            case TokenType.Plus:
            case TokenType.Minus:
            case TokenType.Slash:
                {
                    return ParseComparisonOperators(ref lexer, context, binaryNode);
                }
            case TokenType.Or:
                {
                    return ParseOrOperator(ref lexer, context, binaryNode);
                }
            case TokenType.And:
                {
                    return ParseAndOperator(ref lexer, context, binaryNode);
                }
            default:
                {
                    throw new Exception("");
                }
        }
    }
    private BinaryQueryNode ParseComparisonOperators(ref TokenLexer lexer, ParserContext context, BinaryQueryNode queryNode)
    {
        var binaryOperator = (BinaryOperatorType)lexer.Current.TokenType;
        var token = default(Token);

        if (!lexer.TryNext(out token))
        {
            // TODO: Add Diagnostics
            return queryNode;
        }
        if (token.IsLiteral)
        {
            var constantParser = context.GetParser<ConstantParser>();
            var constantNode = constantParser.Parse<ConstantQueryNode>(ref lexer, context);

            queryNode = new BinaryQueryNode()
            {
                LeftOperand = queryNode.LeftOperand,
                RightOperand = constantNode,
                OperatorType = binaryOperator
            };
        }
        if (token.IsIdentifier)
        {
            if (token.Value.IsFunction(out var functionType))
            {
                queryNode = new BinaryQueryNode()
                {
                    LeftOperand = queryNode.LeftOperand,
                    RightOperand = context.GetParser<FunctionParser>().Parse(ref lexer, context, new FunctionQueryNode()
                    {
                        FunctionType = functionType,
                    }),
                    OperatorType = binaryOperator
                };
            }
            else
            {
                queryNode = new BinaryQueryNode()
                {
                    LeftOperand = queryNode.LeftOperand,
                    RightOperand = context.GetParser<PropertyParser>().Parse<PropertyQueryNode>(ref lexer, context),
                    OperatorType = binaryOperator
                };
            }
        }  
        if (lexer.TryPeek(out token) && token.IsOperator)
        {
            lexer.Next();
            if (Parse(ref lexer, context, new BinaryQueryNode() { LeftOperand = queryNode }) is not BinaryQueryNode binaryNode)
            {
                throw new Exception("");
            }
            queryNode = binaryNode;
        }

        return queryNode;
    }
    private BinaryQueryNode ParseAndOperator(ref TokenLexer lexer, ParserContext context, BinaryQueryNode queryNode)
    {
        var token = default(Token);
        var rightOperand = default(QueryNode);

        if (!lexer.TryNext(out token))
        {
            // TODO: Add Diagnostics
            return queryNode;
        }
        switch (token.TokenType)
        {
            case TokenType.OpenParenthesis:
                {
                    rightOperand = ParseParenthesisBlock(ref lexer, context);
                    break;
                }
            case TokenType.Identifier when token.Value.IsFunction(out var functionType):
                {
                    rightOperand =context.GetParser<FunctionParser>().Parse(ref lexer, context, new FunctionQueryNode()
                    {
                        FunctionType = functionType
                    });
                    break;
                }
            case TokenType.Identifier:
                {
                    rightOperand = context.GetParser<PropertyParser>().Parse<PropertyQueryNode>(ref lexer, context);
                    break;
                }
            // Check for constants on the right side.
            case TokenType.Null:
            case TokenType.FloatingPoint:
            case TokenType.Integer:
            case TokenType.String:
            case TokenType.Boolean:
                {
                    rightOperand = context.GetParser<ConstantParser>().Parse<ConstantQueryNode>(ref lexer, context);
                    break;
                }
        }
        if (lexer.TryPeek(out token) && token.IsOperator)
        {
            lexer.Next();
            rightOperand = Parse(ref lexer, context, new BinaryQueryNode() { LeftOperand = queryNode }) as BinaryQueryNode;
        }

        queryNode = new BinaryQueryNode()
        {
            LeftOperand = queryNode.LeftOperand,
            RightOperand = rightOperand,
            OperatorType = BinaryOperatorType.And
        };

        return queryNode;
    }
    private BinaryQueryNode ParseOrOperator(ref TokenLexer lexer, ParserContext context, BinaryQueryNode queryNode)
    {
        var token = default(Token);
        var rightOperand = default(QueryNode);

        if (!lexer.TryNext(out token))
        {
            // TODO: Add Diagnostics
            return queryNode;
        }
        switch (token.TokenType)
        {
            case TokenType.OpenParenthesis:
                {
                    rightOperand = ParseParenthesisBlock(ref lexer, context);
                    break;
                }
            case TokenType.Identifier when token.Value.IsFunction(out var functionType):
                {
                    rightOperand = context.GetParser<FunctionParser>().Parse(ref lexer, context, new FunctionQueryNode()
                    {
                        FunctionType = functionType
                    });
                    break;
                }
            case TokenType.Identifier:
                {
                    rightOperand = context.GetParser<PropertyParser>().Parse<PropertyQueryNode>(ref lexer, context);
                    break;
                }
            // Check for constants on the right side.
            case TokenType.Null:
            case TokenType.FloatingPoint:
            case TokenType.Integer:
            case TokenType.String:
            case TokenType.Boolean:
                {
                    rightOperand = context.GetParser<ConstantParser>().Parse<ConstantQueryNode>(ref lexer, context);
                    break;
                }
        }
        if (lexer.TryPeek(out token) && token.IsOperator)
        {
            lexer.Next();
            rightOperand = Parse(ref lexer, context, new BinaryQueryNode() { LeftOperand = queryNode }) as BinaryQueryNode;
        }
        queryNode = new BinaryQueryNode()
        {
            LeftOperand = queryNode.LeftOperand,
            RightOperand = rightOperand,
            OperatorType = BinaryOperatorType.Or
        };

        return queryNode;
    }
    private QueryNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context)
    {
        var leftOperand = default(QueryNode);

        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.CloseParenthesis)
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
                        break;
                    }
                case TokenType.Identifier:
                    {
                        leftOperand = context.GetParser<PropertyParser>().Parse<PropertyQueryNode>(ref lexer, context);
                        break;
                    }
                case TokenType.Null:
                case TokenType.String:
                case TokenType.Integer:
                case TokenType.FloatingPoint:
                case TokenType.Boolean:
                    {
                        leftOperand = context.GetParser<ConstantParser>().Parse(ref lexer, context, new ConstantQueryNode());
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
                        leftOperand = Parse(ref lexer, context, new BinaryQueryNode()
                        {
                            LeftOperand = leftOperand
                        });

                        break;
                    }
                case TokenType.OpenParenthesis:
                    {
                        leftOperand = ParseParenthesisBlock(ref lexer, context);
                        break;
                    }

            }
        }

        return leftOperand;
    }
}
