using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;

namespace Assimalign.OGraph.Syntax.Internal;

internal class EdgeParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context)
    {
        Token token;

        // Ensure next token is an Open Parenthesis Block
        if ((lexer.TryNext(out token) && token.TokenType != TokenType.OpenParenthesis) || lexer.Previous.TokenType != TokenType.Edge)
        {
            AddExpectedOpenParenDiagnostic(ref lexer, context);
            return null;
        }

        return ParseParenthesisBlock(ref lexer, context);
    }

    private EdgeNode? ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context)
    {
        Token token;

        LabelNode? label = null;
        VertexNode? source = (context.Parent as VertexNode)!;
        VertexNode? target = null;
        //ConstantNode? argument = null;
        List<QueryNode> nodes = new();
        string alias;
        int index = 0;

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

        // Add Previous path check
        //if (previous is not null && previous != source.Label.Name)
        //{
        //    // Invalid Path
        //    return null;
        //}

        if (token.TokenType == TokenType.Alias)
        {

        }
        if (token.TokenType == TokenType.CloseParenthesis)
        {
            label = new(current!);
            target = new VertexNode(label, nodes);

        }
        if (lexer.TryNext(out token) && token.TokenType != TokenType.Dot)
        {
            AddExpectedDotSeparatorDiagnostic(ref lexer, context);
            return null;
        }

        while (lexer.TryPeek(out token))
        {
            var parser = token.TokenType switch
            {
                TokenType.Page => (Parser)context.GetParser<PageParser>(),
                TokenType.Sort => context.GetParser<EdgeParser>(),
                _ => null
            };

            if (parser is not null)
            {
                lexer.Skip();
                var node = parser.Parse(ref lexer, context);
                if (node is not null)
                {
                    nodes.Add(node);
                }
                continue;
            }

            break;
        }
        return new EdgeNode(label!, source, target!);

        //while (lexer.TryNext(out token))
        //{
        //    var node = token.TokenType switch
        //    {
        //        TokenType.Page => context.GetParser<PageParser>()
        //            .Parse(ref lexer, context),
        //        TokenType.Edge => context.GetParser<EdgeParser>()
        //            .Parse(ref lexer, context),
        //        _ => null
        //    };

        //    if (node is null)
        //    {
        //        // TODO: Add Diagnostics
        //        return null;
        //    }

        //    nodes.Add(node);
        //}

        //while (lexer.TryNext(out token))
        //{
        //    if (token.TokenType != TokenType.Identifier)
        //    {
        //        // TODO: Add Diagnostics
        //        return null;
        //    }

        //    if (!lexer.TryNext(out token))
        //    {
        //        // TODO: Expected slash or comma
        //        return null;
        //    }
        //    if (token.TokenType != TokenType.Slash &&
        //        token.TokenType != TokenType.Alias &&
        //        token.TokenType != TokenType.CloseParenthesis)
        //    {
        //        // TODO : Add Diagnostic - There can't be any other token between the slash and identifier
        //        return null;
        //    }
        //    if (token.TokenType == TokenType.Slash)
        //    {
        //        if (lexer.Previous.TokenType != TokenType.Identifier)
        //        {
        //            //TODO: Add Diagnostic - There can't be any other token between the slash and identifier
        //            return null;
        //        }

        //        index++;

        //        continue;
        //    }
        //    if (index > 0 && lexer.Previous.Text != source.Label.Name)
        //    {
        //        // Invalid Path
        //        return null;
        //    }
        //    if (token.TokenType == TokenType.Alias)
        //    {

        //    }
        //    if (token.TokenType == TokenType.CloseParenthesis)
        //    {
        //        label = new(lexer.Previous.Text);
        //        target = new VertexNode(label, nodes);
        //        break;
        //    }
        //}

        //if (lexer.TryNext(out token) && token.TokenType != TokenType.Dot)
        //{
        //    AddExpectedDotSeparatorDiagnostic(ref lexer, context);
        //    return null;
        //}

        //while (lexer.TryNext(out token))
        //{
        //    context.Parent = target;

        //    var node = token.TokenType switch
        //    {
        //        TokenType.Page => context.GetParser<PageParser>()
        //            .Parse(ref lexer, context),
        //        TokenType.Edge => context.GetParser<EdgeParser>()
        //            .Parse(ref lexer, context),
        //        _ => null
        //    };

        //    if (node is null)
        //    {
        //        // TODO: Add Diagnostics
        //        return null;
        //    }

        //    nodes.Add(node);
        //}






        //return new EdgeNode(label!, source, target!);
    }
}