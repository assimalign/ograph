using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

using Assimalign.OGraph.Syntax.Lexer;
using System.ComponentModel;

internal class RootParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {
        if (node is not RootQueryNode root)
        {
            // TODO: Add diagnostic information
            return node;
        }
        while (lexer.HasNext)
        {
            var token = lexer.Next();

            switch (token.TokenType)
            {
                case TokenType.Page:
                    node = ParsePage(ref lexer, context, root);
                    break;
                case TokenType.Filter:
                    node = ParseFilter(ref lexer, context, root);
                    break;
                case TokenType.Project:
                    node = ParseProject(ref lexer, context, root);
                    break;
                case TokenType.Sort:
                    node = ParseSort(ref lexer, context, root);
                    break;
                case TokenType.QueryRoot:
                    root = ParseParenthesisBlock(ref lexer, context, root);
                    break;
                default:
                    {
                        // TODO: Add Diagnostic information. Unexpected token
                        break;
                    }
            }
        }

        return root;
    }

    private RootQueryNode ParseRoot(ref TokenLexer lexer, ParserContext context, RootQueryNode node)
    {
        while (lexer.HasNext)
        {

        }
    }

    private RootQueryNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context, RootQueryNode node)
    {
        if (!lexer.HasNext)
        {
            // TODO: Add Diagnostic error. Unexpected EOF
            return node;
        }
        var token = lexer.Next();

        if (token.TokenType != TokenType.OpenBracket)
        {
            // TODO: Add diagnostic error. Expected starting bracket block
            return node;
        }

        return ParseBracketBlock(ref lexer, context, node);
    }
    private RootQueryNode ParseBracketBlock(ref TokenLexer lexer, ParserContext context, RootQueryNode node)
    {
        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.CloseBracket)
            {
                break;
            }
            switch (token.TokenType)
            {
                case TokenType.Page:
                    node = ParsePage(ref lexer, context, node);
                    break;
                case TokenType.Filter:
                    node = ParseFilter(ref lexer, context, node);
                    break;
                case TokenType.Project:
                    node = ParseProject(ref lexer, context, node);
                    break;
                case TokenType.Sort:
                    node = ParseSort(ref lexer, context, node);
                    break;
                default:
                    {
                        // TODO: Add Diagnostic information. Unexpected token
                        break;
                    }
            }
        }

        return node;
    }

    private RootQueryNode ParsePage(ref TokenLexer lexer, ParserContext context, RootQueryNode node)
    {
        var parser = context.GetParser<PageParser>();
        var list = node.Nodes.ToList();
        var pageNode = parser.Parse(ref lexer, context, new PageQueryNode());

        if (pageNode is not PageQueryNode)
        {
            // TODO: Add diagnositic information
        }

        list.Add(pageNode);

        return new RootQueryNode()
        {
            Nodes = list
        };
    }

    private RootQueryNode ParseSort(ref TokenLexer lexer, ParserContext context, RootQueryNode node)
    {
        var parser = context.GetParser<PageParser>();

        return default;
    }
    private RootQueryNode ParseProject(ref TokenLexer lexer, ParserContext context, RootQueryNode node)
    {
        var parser = context.GetParser<PageParser>();

        return default;
    }

    private RootQueryNode ParseFilter(ref TokenLexer lexer, ParserContext context, RootQueryNode node)
    {
        var parser = context.GetParser<PageParser>();

        return default;
    }
}
