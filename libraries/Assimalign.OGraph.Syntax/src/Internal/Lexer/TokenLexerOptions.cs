using System;
using System.Text;

namespace Assimalign.OGraph.Syntax.Internal;

internal struct TokenLexerOptions
{
    public TokenLexerOptions()
    {
        this.Encoding = Encoding.UTF8;
    }
    /// <summary>
    /// Skips all tab '\t' Token Types: <see cref="TokenType.Tab"/> 
    /// </summary>
    internal bool SkipTabs { get; set; }
    /// <summary>
    /// Skips all tab ' ' Token Types: <see cref="TokenType.WhiteSpace"/> 
    /// </summary>
    internal bool SkipWhiteSpace { get; set; }
    /// <summary>
    /// Skips all tab '\n' Token Types: <see cref="TokenType.LineFeed"/> 
    /// </summary>
    internal bool SkipLineFeed { get; set; }
    /// <summary>
    /// Skips all tab '\r' Token Types: <see cref="TokenType.CarriageReturn"/> 
    /// </summary>
    internal bool SkipCarriageReturn { get; set; }
    /// <summary>
    /// Skips all tab '\t' Token Types: <see cref="TokenType.Comment"/> 
    /// </summary>
    internal bool SkipComments { get; set; }
    /// <summary>
    /// 
    /// </summary>
    internal Encoding Encoding { get; set; }


    public static TokenLexerOptions Default => new();
}
