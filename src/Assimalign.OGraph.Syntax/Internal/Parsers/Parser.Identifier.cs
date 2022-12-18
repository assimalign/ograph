using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal class IdentifierParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {
        var identifier = lexer.Current switch
        {
            //"any"           => ParseFunctionAny(ref lexer, context, node),
            //"startswith"    => ParseFunctionStartsWtih(ref lexer, context, node),
            //"endswith"      => ParseFunctionEndsWith(ref lexer, context, node),
            _               => ParseMember(ref lexer, context, node),
        };

        // Projection Logic 
        if (identifier is FieldQueryNode fieldNode)
        {
            // Alias's always follow identifiers. Let's check to see if alias is next
            var token = lexer.Peek();

            if (token.TokenType == TokenType.Alias)
            {
                token = lexer.Next();

                //if (context.Parse(ref lexer, fieldNode) is not FieldQueryNode)
                //{
                //    throw QueryParserException.UnexpectedNode();
                //}
            }

            return fieldNode;
        }



        return default;
    }

    private QueryNode ParseMember(ref TokenLexer lexer, ParserContext context, QueryNode fieldNode)
    {
        return new MemberQueryNode(default);
    }

    private QueryNode ParseFunctionAny(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {
        return default;
    }

    private QueryNode ParseFunctionEndsWith(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {

        return default;
    }
    private QueryNode ParseFunctionStartsWtih(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {

        return default;
    }
}
