using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed partial class QueryParser
{


    public QueryNode Parse(string query)
    {
        return Parse(Encoding.UTF8.GetBytes(query));
    }

    public QueryNode Parse(byte[] query)
    {
        var lexer = new TokenLexer(query, new()
        {
            SkipCarriageReturn = true,
            SkipLineFeed = true,
            SkipTabs = true,
            SkipWhiteSpace = true,
        });

        var rootNode = new RootQueryNode();

        while (lexer.HasNext)
        {
            Parse(ref lexer, rootNode);
        }

        return rootNode;
    }


    #region Private Methods
    private QueryNode Parse(ref TokenLexer lexer, QueryNode node)
    {
        return lexer.Next().TokenType switch
        {
            TokenType.Dot               => Parse(ref lexer, node),
            TokenType.Filter            => ParseFilter(ref lexer, node),
            TokenType.Select            => ParseSelect(ref lexer, node),
            TokenType.Page              => ParsePage(ref lexer, node),
            TokenType.Sort              => ParseSort(ref lexer, node),
            TokenType.Identifier        => ParseIdentifier(ref lexer, node),
            // Binary Parse
            TokenType.Equal,
            TokenType.NotEqual,
            TokenType.LessThan,
            TokenType.LessThanOrEqual,
            TokenType.And,
            TokenType.Or                => ParseBinary(ref lexer, node),
            TokenType.OpenParenthesis   => ParseParnthesisBlock(ref lexer, node),
            TokenType.OpenBracket       => ParseBracketBlock(ref lexer, node)
        };
    }
    private QueryNode ParseSort(ref TokenLexer lexer, QueryNode node)
    {
        if (node is not RootQueryNode)
        {
            throw QueryParserException.InvalidPage();
        }

        return default;
    }
    private QueryNode ParsePage(ref TokenLexer lexer, QueryNode node)
    {
        if (node is not RootQueryNode)
        {
            throw QueryParserException.InvalidPage();
        }

        long? take;
        long? skip;
        string token;

        while (lexer.HasNext || lexer.Current.TokenType != TokenType.CloseParenthesis)
        {
            switch (lexer.Current.TokenType)
            {
                case TokenType.OpenParenthesis:
                case TokenType.OpenBracket:
                case TokenType.CloseBracket:
                    continue;
                case TokenType.Take:
                    {
                        var next = lexer.Next();
                        if (next.TokenType != TokenType.Integer)
                        {
                            throw QueryParserException.InvalidPage();
                        }
                        take = next.GetLong();
                        break;
                    }
                case TokenType.Skip:
                    {
                        var next = lexer.Next();
                        if (next.TokenType != TokenType.Integer)
                        {
                            throw QueryParserException.InvalidPage();
                        }
                        skip = next.GetLong();
                        break;
                    }
                case TokenType.Token:
                    {
                        var next = lexer.Next();
                        if (next.TokenType != TokenType.String)
                        {
                            throw QueryParserException.InvalidPage();
                        }
                        skip = next.GetString();
                        break;
                    }
                default:
                    {
                        throw QueryParserException.InvalidPage();
                    }
            }
        }

        return new PageQueryNode()
        {
            Take = take,
            Skip = skip
        };
    }
    private QueryNode ParseFilter(ref TokenLexer lexer, QueryNode node)
    {
        if (node is not RootQueryNode)
        {
            throw QueryParserException.InvalidPage();
        }

        var binaryNode = Parse(ref lexer, default);


        var filterNode = new FilterQueryNode()
        {
            Predicate = binaryNode,
        };

        root.AddNode(filterNode);


        return filterNode;
    }
    private QueryNode ParseSelect(ref TokenLexer lexer, QueryNode node)
    {
        if (node is not RootQueryNode)
        {
            throw QueryParserException.InvalidPage();
        }

        var queryNode = new SelectQueryNode();

        var left = default(QueryNode);


        while (lexer.HasNext)
        {
            switch (lexer.Next().TokenType)
            {
                case TokenType.OpenParenthesis:
                case TokenType.OpenBracket:
                    continue;

                case TokenType.Identifier:
                    {
                        left = ParseIdentifier(ref lexer);

                        if (left.NodeType == QueryNodeType.Function || left.NodeType == QueryNodeType.Member)
                        {

                        }

                    }
                    break;

                case TokenType.Equal:
                case TokenType.NotEqual:
                case TokenType.LessThan:
                case TokenType.LessThanOrEqual:
                case TokenType.GreaterThan:
                case TokenType.GreaterThanOrEqual:
                    {

                        break;
                    }
                case TokenType.And:
                case TokenType.Or:
                    current = ParseBinaryNode(ref lexer, current);
                    break;

                default:
                    throw QueryParserException.UnexpectedToken(default);

            }
        }





        return queryNode;
    }
    private QueryNode ParseParnthesisBlock(ref TokenLexer lexer, QueryNode node)
    {
        while (lexer.Current.TokenType != TokenType.CloseParenthesis)
        {
            switch (node)
            {
                case SelectQueryNode select:
                    {


                        break;
                    }
                default:
                    {
                        return Parse(ref lexer, node);
                    }
            }
        }

        return node;
    }
    private QueryNode ParseBracketBlock(ref TokenLexer lexer, QueryNode node)
    {
        while (lexer.Current.TokenType != TokenType.CloseBracket)
        {

        }

        return default;
    }
    private QueryNode ParseBinary(ref TokenLexer lexer, QueryNode node)
    {
        var token = lexer.Current;

        // Parse Conjunction Binary
        if (token.TokenType == TokenType.And || token.TokenType == TokenType.Or)
        {
            var operatorType = (BinaryOperatorType)token.TokenType;

            // Conjunctions must not be followed by conjunctions (Duplicate binary statement)
            if (node is not BinaryQueryNode leftOperand)
            {
                throw QueryParserException.InvalidBinary();
            }
            if (leftOperand.OperatorType == BinaryOperatorType.And || leftOperand.OperatorType == BinaryOperatorType.Or)
            {
                throw QueryParserException.InvalidBinary();
            }

            var rightOperand = Parse(ref lexer, default);

            return new BinaryQueryNode()
            {
                Left = leftOperand,
                Right = rightOperand,
                OperatorType = operatorType
            };
        }

        // Parse Predicate Binary
        if (token.TokenType == TokenType.Equal || 
            token.TokenType == TokenType.NotEqual || 
            token.TokenType == TokenType.GreaterThan ||
            token.TokenType == TokenType.GreaterThanOrEqual ||
            token.TokenType == TokenType.LessThan ||
            token.TokenType == TokenType.LessThanOrEqual)
        {
            var operatorType = (BinaryOperatorType)token.TokenType;

            if (node is not MemberQueryNode leftOperand || node is not FunctionCallQueryNode leftOperand || node is not ConstantQueryNode leftOperand)
            {
                throw QueryParserException.InvalidBinary();
            }


            var rightOperand = Parse(ref lexer, default);

            return new BinaryQueryNode()
            {
                Left = leftOperand,
                Right = rightOperand,
                OperatorType = operatorType
            };

        }
        

        throw QueryParserException.InvalidBinary();
    }
    private QueryNode ParseIdentifier(ref TokenLexer lexer, QueryNode node)
    {
        return default;
    }
    private QueryNode ParseFunction(ref TokenLexer lexer, QueryNode node)
    {



        // After parsing function check if there is an alias
        if (lexer.TryPeek(out var next) && next.TokenType == TokenType.Alias)
        {

        }

        return default;
    }
    private QueryNode ParseFunctionParameter(ref TokenLexer lexer, QueryNode node)
    {
        return default;
    }
    private QueryNode ParseMember(ref TokenLexer lexer, QueryNode node)
    {

        // After parsing function check if there is an alias
        if (lexer.TryPeek(out var next) && next.TokenType == TokenType.Alias)
        {
            return ParseAlias(ref lexer, default);
        }

        return default;
    
    }

    private QueryNode ParseAlias(ref TokenLexer lexer, QueryNode node)
    {

        return default;
    }

    #endregion
}
