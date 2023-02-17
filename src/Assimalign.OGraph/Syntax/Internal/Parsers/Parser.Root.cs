using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

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
                    root = ParsePage(ref lexer, context, root);
                    break;
                case TokenType.Filter:
                    root = ParseFilter(ref lexer, context, root);
                    break;
                case TokenType.Project:
                    root = ParseProject(ref lexer, context, root);
                    break;
                case TokenType.Sort:
                    root = ParseSort(ref lexer, context, root);
                    break;
                case TokenType.QueryRoot:
                    root = ParseRoot(ref lexer, context, root);
                    break;
                case TokenType.Dot:
                    continue;
                default:
                    {
                        // Add Diagnostic information. Unexpected token
                        context.AddDiagnostic(new Diagnostic()
                        {
                            Severity = DiagnosticSeverity.Error,
                            Location = DiagnosticLocation.Relative,
                            Start = token.Start,
                            End = token.End,
                            Message = $"Unexpected Token: {token}"
                        });
                        break;
                    }
            }
        }

        return root;
    }

    private RootQueryNode ParseRoot(ref TokenLexer lexer, ParserContext context, RootQueryNode node)
    {
        if (!lexer.HasNext)
        {
            // TODO: Add diagnostics unexpected EOF
            return node;
        }

        var token = lexer.Next();

        if (token.TokenType != TokenType.OpenParenthesis)
        {
            // TODO: Add diagnostic error. Expected starting parenthesis block
            return node;
        }

        return ParseParenthesisBlock(ref lexer, context, node);
    }
    private RootQueryNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context, RootQueryNode node)
    {
        if (!lexer.TryPeek(out var next))
        {
            // TODO: Add Diagnostic error. Unexpected EOF
            return node;
        }
        if (next.TokenType != TokenType.OpenBracket && next.TokenType != TokenType.CloseParenthesis)
        {
            // TODO: Add diagnostic error. Expected starting bracket block
            return node;
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

                break;
            }

            node = ParseBracketBlock(ref lexer, context, node);
        }

        return node;
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
        var nodes = node.Nodes.ToList();
        var pageParser = context.GetParser<PageParser>();
        var pageNode = pageParser.Parse(ref lexer, context, new PageQueryNode());

        if (pageNode is not PageQueryNode)
        {
            // TODO: Add diagnositic information
        }

        nodes.Add(pageNode);

        return new RootQueryNode()
        {
            Nodes = nodes
        };
    }
    private RootQueryNode ParseSort(ref TokenLexer lexer, ParserContext context, RootQueryNode node)
    {
        var nodes = node.Nodes.ToList();
        var sortParser = context.GetParser<ProjectionParser>();
        var sortNode = sortParser.Parse(ref lexer, context, new FilterQueryNode());

        if (sortNode is not ProjectionQueryNode)
        {
            // TODO: Add diagnositic information
        }

        nodes.Add(sortNode);

        return new RootQueryNode()
        {
            Nodes = nodes
        };
    }
    private RootQueryNode ParseProject(ref TokenLexer lexer, ParserContext context, RootQueryNode node)
    {
        var nodes = node.Nodes.ToList();
        var projectionParser = context.GetParser<ProjectionParser>();
        var projectionNode = projectionParser.Parse(ref lexer, context, new ProjectionQueryNode());

        if (projectionNode is not ProjectionQueryNode)
        {
            // TODO: Add diagnositic information
        }

        nodes.Add(projectionNode);

        return new RootQueryNode()
        {
            Nodes = nodes
        };
    }
    private RootQueryNode ParseFilter(ref TokenLexer lexer, ParserContext context, RootQueryNode node)
    {
        var nodes = node.Nodes.ToList();
        var filterParser = context.GetParser<ProjectionParser>();
        var filterNode = filterParser.Parse(ref lexer, context, new FilterQueryNode());

        if (filterNode is not ProjectionQueryNode)
        {
            // TODO: Add diagnositic information
        }

        nodes.Add(filterNode);

        return new RootQueryNode()
        {
            Nodes = nodes
        };
    }
}
