using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal static class TokenLexerExtensions
{
    #region Separators
    private static ReadOnlySpan<KeyValuePair<TokenType, byte[]>> separators => new KeyValuePair<TokenType, byte[]>[]
    {
        new KeyValuePair<TokenType, byte[]>(TokenType.WhiteSpace, new byte[] {(byte)' ' }),
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSeparator(this ref SequenceReader<byte> sequenceReader, out TokenType tokenType)
    {
        tokenType = default;

        // Separators are one byte long 
        if (sequenceReader.Consumed != 1)
        {
            return false;
        }

        var value = sequenceReader.Slice().ToArray();

        foreach (var separator in separators)
        {
            if (separator.Value.SequenceEqual(value))
            {
                tokenType = separator.Key;
                return true;
            }
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSeparatorNext(this ref SequenceReader<byte> sequenceReader)
    {
        if (sequenceReader.TryPeek(out var value))
        {
            var array = new byte[] { value };

            foreach (var separator in separators)
            {
                if (separator.Value.SequenceEqual(array))
                {
                    return true;
                }
            }
        }

        return false;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSeparatorNext(this ref SequenceReader<byte> sequenceReader, params byte[] omit)
    {
        if (sequenceReader.TryPeek(out var value))
        {
            var array = new byte[] { value };
            var omits = omit.Select(x=> new byte[] { x });

            foreach (var separator in separators)
            {
                if (omits.Any(x => x.SequenceEqual(separator.Value)))
                {
                    continue;
                }
                if (separator.Value.SequenceEqual(array))
                {
                    return true;
                }
            }
        }

        return false;
    }
    #endregion

    #region Keywords
    private static ReadOnlySpan<KeyValuePair<TokenType, byte[]>> keywords => new KeyValuePair<TokenType, byte[]>[]
    {
        new KeyValuePair<TokenType, byte[]>(TokenType.Filter, new byte[] { (byte)'f', (byte)'i', (byte)'l', (byte)'t', (byte)'e', (byte)'r' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Project, new byte[] { (byte)'p', (byte)'r', (byte)'o', (byte)'j', (byte)'e', (byte)'c', (byte)'t' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Sort, new byte[] { (byte)'s', (byte)'o', (byte)'r', (byte)'t' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Page, new byte[] { (byte)'p', (byte)'a', (byte)'g', (byte)'e' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Ascending, new byte[] { (byte)'a', (byte)'s', (byte)'c' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Descending, new byte[] { (byte)'d', (byte)'e', (byte)'s', (byte)'c' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Take, new byte[] { (byte)'t', (byte)'a', (byte)'k', (byte)'e' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Skip, new byte[] { (byte)'s', (byte)'k', (byte)'i', (byte)'p' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Token, new byte[] { (byte)'t', (byte)'o', (byte)'k', (byte)'e', (byte)'n' })
    };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsKeyword(this ref SequenceReader<byte> sequenceReader, out TokenType tokenType)
    {
        tokenType = default;

        if (sequenceReader.Consumed < 3 || sequenceReader.Consumed > 7)
        {
            return false;
        }

        var value = sequenceReader.SliceToLowerChar().ToArray();

        foreach (var keyword in keywords)
        {
            if (keyword.Value.SequenceEqual(value) &&
               (sequenceReader.IsEndNext() || sequenceReader.IsSeparatorNext()))
            {
                tokenType = keyword.Key;
                return true;
            }
        }

        return false;
    }
    #endregion

    #region Operators
    private static ReadOnlySpan<KeyValuePair<TokenType, byte[]>> operators => new KeyValuePair<TokenType, byte[]>[]
    {
        new KeyValuePair<TokenType, byte[]>(TokenType.Slash, new byte[] {(byte)'/' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Plus, new byte[] { (byte)'+' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Minus, new byte[] { (byte)'-' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Star, new byte[] { (byte)'*' }),
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOperator(this ref SequenceReader<byte> sequenceReader, out TokenType tokenType)
    {
        tokenType = default;
        // No need to check operator since all operators are less than 4 bytes in length
        if (sequenceReader.Consumed > 3)
        {
            return false;
        }

        var value = sequenceReader.SliceToLowerChar().ToArray();

        foreach (var @operator in operators)
        {
            if (@operator.Value.SequenceEqual(value) && (sequenceReader.IsEndNext() || sequenceReader.IsSeparatorNext()))
            {
                tokenType = @operator.Key;
                return true;
            }
        }

        return false;
    }
    #endregion

    #region Literal
    private static ReadOnlySpan<KeyValuePair<TokenType, byte[]>> literals => new KeyValuePair<TokenType, byte[]>[]
    {
        new KeyValuePair<TokenType, byte[]>(TokenType.Boolean, new byte[] { (byte)'t', (byte)'r', (byte)'u', (byte)'e' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Boolean, new byte[] { (byte)'f', (byte)'a', (byte)'l', (byte)'s', (byte)'e' }),
        new KeyValuePair<TokenType, byte[]>(TokenType.Null, new byte[] { (byte)'n', (byte)'u', (byte)'l', (byte)'l' }),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLiteral(this ref SequenceReader<byte> sequenceReader, out TokenType tokenType)
    {
        tokenType = default;

        if (sequenceReader.Consumed == 1)
        {
            // Identify if string literal
            if (sequenceReader.CurrentSpan[0] == (byte)'\'')
            {
                if (!sequenceReader.TryAdvanceTo((byte)'\''))
                {
                    throw new Exception("Invalid string format");
                }
                tokenType = TokenType.String;
                return true;
            }

            // Identify if current value is digit
            if (char.IsDigit((char)sequenceReader.CurrentSpan[0]))
            {
                while (sequenceReader.TryRead(out var c))
                {
                    if (!char.IsDigit((char)c) && c != (byte)'.' && c != (byte)'e') // Lets check that the current span includes acceptable char
                    {
                        sequenceReader.Rewind(1);
                        break;
                    }
                }

                var value = sequenceReader.Slice();
                foreach (var v in value)
                {
                    if (v == (byte)'.')
                    {
                        tokenType = TokenType.FloatingPoint;
                        return true;
                    }
                }

                tokenType = TokenType.Integer;
                return true;
            }
        }
        // Identify keyword literals
        else
        {
            var value = sequenceReader.SliceToLowerChar().ToArray();

            foreach (var literal in literals)
            {
                if (literal.Value.SequenceEqual(value) && (sequenceReader.IsEndNext() || sequenceReader.IsSeparatorNext()))
                {
                    tokenType = literal.Key;
                    return true;
                }
            }
        }

        return false;
    }
    #endregion

    #region Identifier

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsIdentifer(this ref SequenceReader<byte> sequenceReader, out TokenType tokenType)
    {
        tokenType = default;

        // As the lexer loops through the sequence of bytes
        if (sequenceReader.IsSeparatorNext() || 
            sequenceReader.IsEndNext() || 
            !sequenceReader.IsAlphaNumericCharNext()) // This is to account for any unknown char
        {
            tokenType = TokenType.Identifier;
            return true;
        }

        return false;
    }

    #endregion

    #region Other

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsAlphaNumericCharNext(this ref SequenceReader<byte> sequenceReader) => sequenceReader.TryPeek(out var value) && char.IsLetterOrDigit((char)value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsEndNext(this ref SequenceReader<byte> sequenceReader) => sequenceReader.Remaining <= 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<byte> Slice(this ref SequenceReader<byte> sequenceReader) => sequenceReader.CurrentSpan.Slice(0, (int)sequenceReader.Consumed);

    // Slices current consumed sequence as span and convert all alpha-characters to lower case if not already
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ReadOnlySpan<byte> SliceToLowerChar(this ref SequenceReader<byte> sequenceReader)
    {
        var buffer = new byte[sequenceReader.Consumed];

        for (int i = 0; i < sequenceReader.Consumed; i++)
        {
            buffer[i] = (byte)(char.ToLower((char)sequenceReader.CurrentSpan[i]));
        }

        return buffer.AsSpan();
    }
    #endregion
}
