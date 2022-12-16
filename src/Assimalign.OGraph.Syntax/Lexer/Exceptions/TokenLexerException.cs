using System;

namespace Assimalign.OGraph.Syntax;

public class TokenLexerException : Exception
{
    public TokenLexerException() { }
    internal TokenLexerException(string message) : base(message) { }
    internal TokenLexerException(string message, Exception innerException) : base(message, innerException) { }


    public int Position { get; init; }
}
