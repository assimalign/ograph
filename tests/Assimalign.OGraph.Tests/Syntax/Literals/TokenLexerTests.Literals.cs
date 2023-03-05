using System;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;


public partial class TokenLexerTests
{

    #region String Literals
    [Fact(DisplayName = "Literal Test (String): Bad formatted string value")]
    public void TestBadStringFormatException()
    {
        var exception1 = Assert.Throws<TokenLexerException>(() =>
        {
            var value = "'This is a bad formatted string"; // missing closing single quote
            var lexer = new TokenLexer(value);
            lexer.Next();
        });

        var exception2 = Assert.Throws<TokenLexerException>(() =>
        {
            var value = "\"This is a bad formatted string"; // missing closing double quote
            var lexer = new TokenLexer(value);
            lexer.Next();
        });
    }

    [Fact(DisplayName = "Literal Test (String): Parse Double Quoted String Successfully")]
    public void TestDoubleQuotedStringParsedSuccess()
    {
        var value = "\"This is a string\"";
        var lexer = new TokenLexer(value);
        var token = lexer.Next();

        Assert.Equal(TokenType.String, token.TokenType);
        Assert.Equal("This is a string", token.Text);
    }

    [Fact(DisplayName = "Literal Test (String): Parse Single Quoted String Successfully")]
    public void TestSingleQuotedStringParsedSuccess()
    {
        var value = "'This is a string'";
        var lexer = new TokenLexer(value);
        var token = lexer.Next();

        Assert.Equal(TokenType.String, token.TokenType);
        Assert.Equal("This is a string", token.Text);
    }

    #endregion

    #region Keyword Literals
    [Fact(DisplayName = "Literal Test (Boolean): 'Null' value parsed successfully")]
    public void TestNullLiteralParsedSuccesfully()
    {
        var value = "null";
        var lexer = new TokenLexer(Encoding.UTF8.GetBytes(value));
        var token = lexer.Next();

        Assert.Equal(TokenType.Null, token.TokenType);
    }

    [Fact(DisplayName = "Literal Test (Boolean): 'False' value parsed successfully")]
    public void TestBooleanFalseLiteralParsedSuccesfully()
    {
        var value = "false";
        var lexer = new TokenLexer(Encoding.UTF8.GetBytes(value));
        var token = lexer.Next();

        Assert.Equal(TokenType.Boolean, token.TokenType);
        Assert.True(token.Value.Span.SequenceEqual(new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes("false"))));
    }

    [Fact(DisplayName = "Literal Test (Boolean): 'True' value parsed successfully")]
    public void TestBooleanTrueLiteralParsedSuccesfully()
    {
        var value = "true";
        var lexer = new TokenLexer(Encoding.UTF8.GetBytes(value));
        var token = lexer.Next();

        Assert.Equal(TokenType.Boolean, token.TokenType);
        Assert.True(token.Value.Span.SequenceEqual(new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes("true"))));
    }

    [Fact(DisplayName = "Literal Test (Boolean): 'True' value as prefix to word not identified as Boolean")]
    public void TestBooleanTrueLiteralParsedWithMatchingPrefix()
    {
        var value = "falsely"; // Doubt this will every be a field name, but just encase
        var lexer = new TokenLexer(Encoding.UTF8.GetBytes(value));
        var token = lexer.Next();

        Assert.NotEqual(TokenType.Boolean, token.TokenType);
    }

    #endregion
}
