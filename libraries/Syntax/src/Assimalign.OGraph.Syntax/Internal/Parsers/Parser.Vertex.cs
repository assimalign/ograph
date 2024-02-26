using System;
using System.Collections.Generic;
using System.Linq;

namespace Assimalign.OGraph.Syntax.Internal;

internal class VertexParser : Parser
{
    internal override VertexNode? Parse(ref TokenLexer lexer, ParserContext context)
    {
        Token token;

        // Vertex Args
        LabelNode? label;
        VertexNode vertex;
        ConstantNode? argument = null;

        // Vertex Children
        var nodes = new List<QueryNode>();

        // Ensure next token is an Open Parenthesis Block
        if ((lexer.TryNext(out token) && token.TokenType != TokenType.OpenParenthesis) || lexer.Previous.TokenType != TokenType.Vertex)
        {
            AddExpectedOpenParenDiagnostic(ref lexer, context);
            return null;
        }

        // Get Vertex Identifier
        if (lexer.TryNext(out token) && token.TokenType != TokenType.Identifier)
        {
            // TODO: Add Diagnostics
            return null;
        }

        label = new LabelNode(token.Text);

        var hasNext = lexer.TryNext(out token);

        if (!hasNext && token.TokenType != TokenType.CloseParenthesis && token.TokenType != TokenType.Comma)
        {
            AddExpectedClosingParenDiagnostic(ref lexer, context);
            return null;
        }

        // Check for Argument
        if (token.TokenType == TokenType.Comma)
        {
            if (lexer.TryNext(out token) && (
                token.TokenType == TokenType.String ||
                token.TokenType == TokenType.FloatingPoint ||
                token.TokenType == TokenType.Integer))
            {
                argument = (ConstantNode)context.GetParser<ConstantParser>()
                    .Parse(ref lexer, context);

                lexer.TryNext(out token);
            }
        }
        if (token.TokenType != TokenType.CloseParenthesis)
        {
            AddExpectedClosingParenDiagnostic(ref lexer, context);
            return null;
        }

        vertex = argument is null
            ? new VertexNode(label, nodes)
            : new VertexNode(label, argument, nodes);

        if (lexer.TryNext(out token) && token.TokenType != TokenType.Dot)
        {
            AddExpectedDotSeparatorDiagnostic(ref lexer, context);
            return null;
        }

        while (lexer.TryNext(out token))
        {
            context.Parent = vertex;

            var node = token.TokenType switch
            {
                //TokenType.Sort
                TokenType.Page => context.GetParser<PageParser>()
                    .Parse(ref lexer, context),

                TokenType.Sort => null,
                TokenType.Project => null,



                TokenType.Edge => context.GetParser<EdgeParser>()
                    .Parse(ref lexer, context),
                _ => null
            };

            if (node is null)
            {
                // TODO: Add Diagnostics
                return null;
            }

            nodes.Add(node);
        }

        return vertex;
    }

    private EdgeNode ParseEdge(ref TokenLexer lexer, ParserContext context)
    {
        Token token;

        string? previous = null;
        string? current = null;

        while (lexer.TryNext(out token))
        {
            if (token.TokenType != TokenType.Identifier)
            {
                // TODO: Add Diagnostics
                return null;
            }
            else
            {
                previous = current;
                current = token.Text;
            }
            if (previous is not null)
            {
                var parent = source.Nodes.FirstOrDefault(p => p is EdgeNode edge && edge?.Label?.Name == previous) as EdgeNode;

                if (parent is null)
                {
                    // Invalid Path
                    return null;
                }

                source = parent.Target;
            }
            if (!lexer.TryNext(out token))
            {
                // TODO: Expected slash or comma
                return null;
            }
            if (token.TokenType == TokenType.Slash)
            {
                if (lexer.Previous.TokenType != TokenType.Identifier)
                {
                    //TODO: Add Diagnostic - There can't be any other token between the slash and identifier
                    return null;
                }
                continue;
            }
            else
            {
                break;
            }
        }
    }


}