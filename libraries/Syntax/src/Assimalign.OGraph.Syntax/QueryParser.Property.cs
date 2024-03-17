using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public sealed partial class QueryParser
{
    private PropertyNode? ParseProperty(ref TokenLexer lexer, ParserContext context)
    {
        Token token = lexer.Current;
        Int32 start = lexer.Current.Start;
        Int32 startLine = lexer.Current.Line;
        Int32 end;
        Int32 endLine;
        String text;
        Location location;

        string? name = lexer.Current.Text;
        string? alias;

        // Ensure current token is Identifier
        if (token.TokenType != TokenType.Identifier)
        {
            // TODO: Expected Identifier Diagnostic
            return null;
        }

        // Check for an alias or nested property
        if (lexer.TryPeek(out token) && token.TokenType == TokenType.Alias || token.TokenType == TokenType.OpenBracket)
        {
            token = lexer.Next();
        }
        else
        {
            end = lexer.Position;
            endLine = lexer.Line;
            text = lexer.GetText(start, end);
            location = Location.Create(startLine, endLine, start, end);

            return new PropertyNode(
                name,
                text,
                location);
        }
        if (token.TokenType == TokenType.Alias)
        {
            if (!lexer.TryPeek(out token) || token.TokenType != TokenType.Identifier)
            {
                // TODO: Expected Identifier
                return null;
            }

            token = lexer.Next();
            alias = token.Text;

            // Check for nested properties following alias
            if (lexer.TryPeek(out token) && token.TokenType == TokenType.OpenBracket)
            {
                lexer.Skip();

                var properties = new List<PropertyNode>();

                while (lexer.TryNext(out token))
                {
                    if (token.TokenType == TokenType.CloseBracket)
                    {
                        end = lexer.Position;
                        endLine = lexer.Line;
                        text = lexer.GetText(start, end);
                        location = Location.Create(startLine, endLine, start, end);

                        return new PropertyNode(
                            name,
                            alias,
                            properties,
                            text,
                            location);
                    }
                    if (token.TokenType == TokenType.Identifier)
                    {
                        var property = ParseProperty(ref lexer, context);

                        if (property is null)
                        {
                            continue;
                        }

                        properties.Add(property);
                    }
                    else
                    {
                        SkipToClosingBracket(ref lexer);

                        // TODO: Add Diagnostics

                        end = lexer.Position;
                        endLine = lexer.Line;
                        text = lexer.GetText(start, end);
                        location = Location.Create(startLine, endLine, start, end);

                        return new PropertyNode(
                            name,
                            alias,
                            properties,
                            text,
                            location);
                    }
                }
            }

            end = lexer.Position;
            endLine = lexer.Line;
            text = lexer.GetText(start, end);
            location = Location.Create(startLine, endLine, start, end);

            return new PropertyNode(
                name,
                alias,
                text,
                location);
        }
        if (token.TokenType == TokenType.OpenBracket)
        {
            var properties = new List<PropertyNode>();

            while (lexer.TryNext(out token))
            {
                if (token.TokenType == TokenType.CloseBracket)
                {
                    end = lexer.Position;
                    endLine = lexer.Line;
                    text = lexer.GetText(start, end);
                    location = Location.Create(startLine, endLine, start, end);

                    return new PropertyNode(
                        name,
                        properties,
                        text,
                        location);
                }
                if (token.TokenType == TokenType.Identifier)
                {
                    var property = ParseProperty(ref lexer, context);

                    if (property is null)
                    {
                        continue;
                    }

                    properties.Add(property!);
                }
                else
                {
                    SkipToClosingBracket(ref lexer);
                    // TODO: Add Diagnostics

                    end = lexer.Position;
                    endLine = lexer.Line;
                    text = lexer.GetText(start, end);
                    location = Location.Create(startLine, endLine, start, end);

                    return new PropertyNode(
                        name,
                        properties,
                        text,
                        location);
                }
            }
        }

        AddEofDiagnostic(ref lexer, context);
        return null;
    }
}
