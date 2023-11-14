using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public partial class TokenLexerTests
{
    [Fact(DisplayName = "Projections Test: Keyword Identified Successfully")]
    public void TestProjectionKeywordIdentifiedSuccess()
    {
        var query = "project({firstName})";
        var lexer = new TokenLexer(query);
        var tokens = new List<Token>();

        while (lexer.HasNext)
        {
            tokens.Add(lexer.Next());
        }

        Assert.Contains(tokens, token => token.TokenType == TokenType.Project);
        Assert.Single(tokens, token => token.TokenType == TokenType.Identifier);

        Assert.Single(tokens, token => token.TokenType == TokenType.OpenParenthesis);
        Assert.Single(tokens, token => token.TokenType == TokenType.CloseParenthesis);

        Assert.Single(tokens, token => token.TokenType == TokenType.OpenBracket);
        Assert.Single(tokens, token => token.TokenType == TokenType.CloseBracket);
    }
}
