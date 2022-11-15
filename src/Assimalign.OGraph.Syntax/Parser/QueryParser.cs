using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed class QueryParser
{
    public QueryParser()
    {

    }

    public QueryTree Parse(string query) => Parse(Encoding.Default.GetBytes(query));
    public QueryTree Parse(byte[] query)
    {
        var lexer = new TokenLexer(query, new()
        {
            SkipCarriageReturn = true,
            SkipLineFeed = true,
            SkipTabs = true,
            SkipWhiteSpace = true,
        });
        var nodes = new List<QueryNode>();
        var tree = new QueryTree();

        while (lexer.HasNext)
        {
            var token = lexer.Next();

            switch (token.TokenType)
            {
                case TokenType.Sort:
                    {

                        break;
                    }
                case TokenType.Page:
                    {

                        break;
                    }
                case TokenType.Select:
                    {

                        break;
                    }
                case TokenType.Filter:
                    {
                        nodes.Add(ParseFilter(ref lexer));
                        break;
                    }
                case TokenType.Dot:
                    continue;
                default:
                    {
                        // If any other token is generated then what is in switch statement above,
                        // then it is safe to assume 
                        throw new Exception();
                    }
            }
        }

        return tree;
    }
    private void SkipFormatting(ref TokenLexer lexer)
    {
        var token = lexer.Peek();

        switch (token.TokenType)
        {
            case TokenType.WhiteSpace:
            case TokenType.Tab:
            case TokenType.LineFeed:
            case TokenType.CarriageReturn:
                {
                    lexer.Next();
                    SkipFormatting(ref lexer);
                    break;
                }
            default:
                break;
        }
    }


    private FilterQueryNode ParseFilter(ref TokenLexer lexer)
    {
        var token = lexer.Next();

        switch (token.TokenType)
        {
            case TokenType.Sort:
                {

                    break;
                }
            case TokenType.Page:
                {

                    break;
                }
            case TokenType.Select:
                {

                    break;
                }
            case TokenType.Filter:
                {
                    
                    break;
                }
            default:
                {
                    // If any other token is generated then what is in switch statement above,
                    // then it is safe to assume 
                    throw new Exception();
                }
        }

        var openParan = lexer.Next();
        SkipFormatting(ref lexer);
        var openBlock = lexer.Next();

        if (openParan.TokenType != TokenType.OpenParenthesis || openBlock.TokenType != TokenType.OpenBracket)
        {
            throw new Exception("Invalid query format");
        }




        return default;
    }

    private QueryNode ParseFilterBlock(ref TokenLexer lexer)
    {


        return default;
    }

    private FunctionCallQueryNode ParseFunctionCall(ref TokenLexer lexer)
    {


        return default;
    }
}
