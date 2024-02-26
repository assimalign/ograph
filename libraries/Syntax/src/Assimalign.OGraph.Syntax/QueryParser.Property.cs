using Assimalign.OGraph.Syntax.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed partial class QueryParser
{
    private PropertyNode? ParseProperty(ref TokenLexer lexer, ParserContext context)
    {
        Token token;

        string? name = null;
        string? alias = null;

        if (lexer.Current.TokenType != TokenType.Identifier)
        {
            return null;
        }
        else
        {
            name = lexer.Current.Text;
        }
        if (lexer.TryPeek(out token) && token.TokenType == TokenType.Alias || token.TokenType == TokenType.OpenBracket)
        {
            token = lexer.Next();
        }
        else
        {
            return new PropertyNode(name);
        }
        if (token.TokenType == TokenType.Alias)
        {
            if (!lexer.TryPeek(out token) || token.TokenType != TokenType.Identifier)
            {
                // TODO: Expected Identifier
                return null;
            }
            else
            {
                token = lexer.Next();
                alias = token.Text;
            }
            if (lexer.TryPeek(out token) && token.TokenType == TokenType.OpenBracket)
            {
                token = lexer.Next();
            }
            else
            {
                return new PropertyNode(name, alias);
            }
        }
        if (token.TokenType == TokenType.OpenBracket)
        {
            var children = new List<PropertyNode>();

            while (lexer.TryNext(out token))
            {
                if (token.TokenType == TokenType.Identifier)
                {
                    var child = ParseProperty(ref lexer, context);

                    children.Add(child!);
                }
                if (token.TokenType == TokenType.CloseBracket)
                {
                    if (string.IsNullOrEmpty(alias))
                    {
                        return new PropertyNode(
                            name,
                            children);
                    }
                    return new PropertyNode(name, alias, children);
                }
            }
        }

        AddEofDiagnostic(ref lexer, context);
        return null;
    }
}
