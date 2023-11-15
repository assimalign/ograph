using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public partial class TokenLexerTests
{
    [Fact(DisplayName = "Edge Test: Keyword Identified Successfully")]
    public void TestEdgeKeywordIdentifiedSuccess()
    {
        var query = "edge(companies/addresses)";
        var lexer = new TokenLexer(query);
        var tokens = new List<Token>();

        while (lexer.HasNext)
        {
            tokens.Add(lexer.Next());
        }

        Assert.Contains(tokens, token => token.TokenType == TokenType.Edge);

        // companies/addresses x 2 identifiers
        Assert.True(tokens.Where(token => token.TokenType == TokenType.Identifier).Count() == 2);

        Assert.Single(tokens, token => token.TokenType == TokenType.OpenParenthesis);
        Assert.Single(tokens, token => token.TokenType == TokenType.CloseParenthesis);
    }
}
