using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

using Assimalign.OGraph.Syntax.Lexer;

internal class ProjectionBracketBlockParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {
        // Project should be the current left node

        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (node is ProjectionQueryNode projection) // Parsing Root
            {
                if (token.TokenType == TokenType.CloseBracket)
                {
                    return projection;
                }
                if (context.Parse(ref lexer, projection) is not ProjectionQueryNode)
                {
                    throw QueryParserException.UnexpectedNode();
                }
            }
            if (node is FieldQueryNode field) // Nested select
            {
                if (token.TokenType == TokenType.CloseBracket)
                {
                    return field;
                }
                if (context.Parse(ref lexer, new FieldQueryNode()) is not FieldQueryNode projectionField)
                {
                    throw QueryParserException.UnexpectedNode();
                }

                field.AddChild(projectionField);
            }
        }

        throw QueryParserException.UnexpectedNode();
    }
}
