using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public partial class TokenLexerTests
{

    [Fact(DisplayName = "Comment Test: Identify Comment Successfully")]
    public void TestCommentIdentifiedSuccessfully()
    {
        var query = "project({}) # Some comment I am placing \r\n .filter({})";

        var bytes = Encoding.UTF8.GetBytes(query);
        var lexer = new TokenLexer(bytes);
        var tokens = new List<Token>();

        while (lexer.HasNext)
        {
            var token = lexer.Next();
            tokens.Add(token);
        }

        Assert.Contains(tokens, token => token.TokenType == TokenType.Comment);
        Assert.Contains(tokens, token => token.TokenType == TokenType.Project);
        Assert.Contains(tokens, token => token.TokenType == TokenType.Filter);
    }


    [Fact (DisplayName = "Comment Test: Identify comment with no new line")]
    public void IdentifyCommentEndOfFileTest()
    {
        var query = "project({}) # Some comment I am placing";

        var bytes = Encoding.UTF8.GetBytes(query);
        var lexer = new TokenLexer(bytes);
        var tokens = new List<Token>();

        while (lexer.HasNext)
        {
            var token = lexer.Next();
            tokens.Add(token);
        }

        Assert.Contains(tokens, token => token.TokenType == TokenType.Comment);
        Assert.Contains(tokens, token => token.TokenType == TokenType.Project);
    }


    [Fact (DisplayName = "Comment Test: Skip all comments successfully")]
    public void SkipCommentTest()
    {
        var query = "project({}) # Some comment I am placing";

        var bytes = Encoding.UTF8.GetBytes(query);
        var lexer = new TokenLexer(bytes, new TokenLexerOptions()
        {
            SkipComments = true
        });
        var tokens = new List<Token>();

        while (lexer.HasNext)
        {
            var token = lexer.Next();
            tokens.Add(token);
        }


        Assert.Contains(tokens, token => token.TokenType != TokenType.Comment);
        Assert.Contains(tokens, token => token.TokenType == TokenType.Project);
    }
}
