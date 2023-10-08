using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal static class TokenSpans
{
    internal static ReadOnlySpan<KeyValuePair<TokenType, byte[]>> Separators => new[]
    {
        new KeyValuePair<TokenType, byte[]>(TokenType.WhiteSpace, new byte[] {(byte)' ' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Slash, new byte[] {(byte)'/' }), // This is a type of separator used for edge paths
        new KeyValuePair<TokenType, byte[]>(TokenType.Tab, new byte[] {(byte)'\t' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.LineFeed, new byte[] {(byte)'\n' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.CarriageReturn, new byte[] {(byte)'\r' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.OpenParenthesis, new byte[] {(byte)'(' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.CloseParenthesis, new byte[] {(byte)')' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.OpenBracket,new byte[] { (byte)'{' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.CloseBracket, new byte[] {(byte)'}' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Colon,new byte[] { (byte)':' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Semicolon, new byte[] {(byte)';' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Comma,new byte[] { (byte)',' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Dot,new byte[] { (byte)'.' })
    };
    internal static ReadOnlySpan<KeyValuePair<TokenType, byte[]>> Keywords => new[]
    {
        //new KeyValuePair<TokenType, byte[]>(TokenType.QueryRoot, new byte[] { (byte)'q', (byte)'u', (byte)'e', (byte)'r', (byte)'y' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Filter, new byte[] { (byte)'f', (byte)'i', (byte)'l', (byte)'t', (byte)'e', (byte)'r' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Project, new byte[] { (byte)'p', (byte)'r', (byte)'o', (byte)'j', (byte)'e', (byte)'c', (byte)'t' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Edge, new byte[] { (byte)'e', (byte)'d', (byte)'g', (byte)'e' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Sort, new byte[] { (byte)'s', (byte)'o', (byte)'r', (byte)'t' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Page, new byte[] { (byte)'p', (byte)'a', (byte)'g', (byte)'e' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Ascending, new byte[] { (byte)'a', (byte)'s', (byte)'c' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Descending, new byte[] { (byte)'d', (byte)'e', (byte)'s', (byte)'c' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Take, new byte[] { (byte)'t', (byte)'a', (byte)'k', (byte)'e' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Skip, new byte[] { (byte)'s', (byte)'k', (byte)'i', (byte)'p' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Token, new byte[] { (byte)'t', (byte)'o', (byte)'k', (byte)'e', (byte)'n' })
    };
    internal static ReadOnlySpan<KeyValuePair<TokenType, byte[]>> Operators => new[]
    {
        // arithmetic operators
        new KeyValuePair<TokenType, byte[]>(TokenType.Slash, new byte[] {(byte)'/' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Plus, new byte[] { (byte)'+' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Minus, new byte[] { (byte)'-' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Star, new byte[] { (byte)'*' }),
        // logical operators
        new KeyValuePair<TokenType, byte[]>(TokenType.Equal, new byte[] { (byte)'e', (byte)'q' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.NotEqual, new byte[] { (byte)'n', (byte)'e', (byte)'q' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.GreaterThan, new byte[] { (byte)'g', (byte)'t' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.GreaterThanOrEqual, new byte[] { (byte)'g', (byte)'t', (byte)'e' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.LessThan, new byte[] { (byte)'l', (byte)'t' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.LessThanOrEqual, new byte[] { (byte)'l', (byte)'t', (byte)'e' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Alias, new byte[] { (byte)'a', (byte)'s' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.And, new byte[] { (byte)'a', (byte)'n', (byte)'d' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Or, new byte[] { (byte)'o', (byte)'r' })
    };

    internal static ReadOnlySpan<KeyValuePair<TokenType, byte[]>> Literals => new KeyValuePair<TokenType, byte[]>[]
    {
        new KeyValuePair<TokenType, byte[]>(TokenType.Boolean, new byte[] { (byte)'t', (byte)'r', (byte)'u', (byte)'e' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Boolean, new byte[] { (byte)'f', (byte)'a', (byte)'l', (byte)'s', (byte)'e' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Null, new byte[] { (byte)'n', (byte)'u', (byte)'l', (byte)'l' }),
    };
}
