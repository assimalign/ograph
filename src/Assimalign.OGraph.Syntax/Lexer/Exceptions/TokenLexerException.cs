using System;

namespace Assimalign.OGraph.Syntax;

public abstract class TokenLexerException : Exception
{
    public TokenLexerException() { }
    public TokenLexerException(string message) : base(message) { }
    public TokenLexerException(string message, Exception innerException) : base(message, innerException) { }
}
