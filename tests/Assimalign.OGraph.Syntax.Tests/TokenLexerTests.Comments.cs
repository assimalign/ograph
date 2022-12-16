
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Tests;

using Assimalign.OGraph.Syntax.Lexer;

public partial class TokenLexerTests
{

    [Fact]
    public void IdentifyCommentTest()
    {
        var query = "query({}) # Some comment I am placing \r\n .filter({})";

        var bytes = Encoding.UTF8.GetBytes(query);
        var lexer = new TokenLexer(bytes);
        var tokens = new List<Token>();

        while (lexer.HasNext)
        {
            var token = lexer.Next();
            tokens.Add(token);
        }

        Assert.Contains(tokens, token => token.TokenType == TokenType.Comment);
        Assert.Contains(tokens, token => token.TokenType == TokenType.QueryRoot);
        Assert.Contains(tokens, token => token.TokenType == TokenType.Filter);
    }


    [Fact]
    public void IdentifyCommentEndOfFileTest()
    {
        var query = "query({}) # Some comment I am placing";

        var bytes = Encoding.UTF8.GetBytes(query);
        var lexer = new TokenLexer(bytes);
        var tokens = new List<Token>();

        while (lexer.HasNext)
        {
            var token = lexer.Next();
            tokens.Add(token);
        }

        Assert.Contains(tokens, token => token.TokenType == TokenType.Comment);
        Assert.Contains(tokens, token => token.TokenType == TokenType.QueryRoot);
    }


    [Fact]
    public void SkipCommenTest()
    {
        var query = "query({}) # Some comment I am placing";

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
        Assert.Contains(tokens, token => token.TokenType == TokenType.QueryRoot);
    }
}
