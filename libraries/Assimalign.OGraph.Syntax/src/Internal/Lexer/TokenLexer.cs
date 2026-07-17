using System;
using System.Linq;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Assimalign.OGraph.Syntax.Internal;

/// <summary>
/// The Token Lexer (also known as a Tokenizer and)
/// </summary>
internal ref partial struct TokenLexer
{
    private readonly TokenLexerOptions options;
    private readonly ReadOnlySpan<byte> data;

    private ReadOnlySequence<byte> remaining;
    private Token current = default;
    private Token previous = default;

    private int position = default;

    // Tracks the line position, if any, as the query is parsed.
    private int line = 1;

    public TokenLexer(byte[] query)
    {
        this.options = TokenLexerOptions.Default;
        this.data = query;
        this.remaining = new ReadOnlySequence<byte>(query);
    }
    public TokenLexer(byte[] query, TokenLexerOptions options) : this(query)
    {
        this.options = options;
    }

    #region Public Methods
    /// <summary>
    /// Specifies whether the Lexer has another token within the sequence.
    /// </summary>
    public bool HasNext
    {
        get
        {
            var sequence = remaining; // Need to copy sequence into variable since we do not want to advance to the next sequence
            var sequenceReader = new SequenceReader<byte>(sequence);

            while (!sequenceReader.End)
            {
                sequenceReader.Advance(1);

                if (TryParse(ref sequenceReader, out var token))
                {
                    sequence = sequenceReader.UnreadSequence;

                    switch (token.TokenType)
                    {
                        case TokenType.Tab when options.SkipTabs:
                        case TokenType.LineFeed when options.SkipLineFeed:
                        case TokenType.WhiteSpace when options.SkipWhiteSpace:
                        case TokenType.CarriageReturn when options.SkipCarriageReturn:
                        case TokenType.Comment when options.SkipComments:
                            if (sequence.IsEmpty)
                            {
                                return false;
                            }
                            sequenceReader = new(sequence); // Reset sequence
                            break;
                        default:
                            return true;
                    }
                }
            }

            return false;
        }
    }
    /// <summary>
    /// The current line number the lexer is on.
    /// </summary>
    public readonly int Line => line;
    /// <summary>
    /// Is the current token that has been parsed.
    /// </summary>
    public readonly Token Current => current;
    /// <summary>
    /// Get's the previous token.
    /// </summary>
    public readonly Token Previous => previous;
    /// <summary>
    /// Gets the current byte position of the reader.
    /// </summary>
    public readonly int Position => position;
    /// <summary>
    /// The original raw data.
    /// </summary>
    public ReadOnlySpan<byte> ValueSpan => data;
    /// <summary>
    /// Peeks at the next token in the sequence.
    /// </summary>
    /// <returns></returns>
    public Token Peek()
    {
        var sequence = remaining; // Need to copy sequence into variable since we do not want to advance
        var sequenceReader = new SequenceReader<byte>(sequence);

        while (!sequenceReader.End)
        {
            sequenceReader.Advance(1);

            if (TryParse(ref sequenceReader, out var token))
            {
                sequence = sequenceReader.UnreadSequence;

                switch (token.TokenType)
                {
                    case TokenType.Tab when options.SkipTabs:
                    case TokenType.LineFeed when options.SkipLineFeed:
                    case TokenType.WhiteSpace when options.SkipWhiteSpace:
                    case TokenType.CarriageReturn when options.SkipCarriageReturn:
                    case TokenType.Comment when options.SkipComments:
                        sequenceReader = new(sequence); // Reset sequence
                        break;
                    default:
                        return token;
                }
            }
        }
        // If we reached here something is wrong within the syntax
        throw new TokenLexerException("End of Sequence. No more tokens available.");
    }
    /// <summary>
    /// Retrieves the next token in the sequence.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="TokenLexerException"></exception>
    public Token Next()
    {
        var reader = new SequenceReader<byte>(remaining);

        while (!reader.End)
        {
            reader.Advance(1);

            if (TryParse(ref reader, out var token))
            {
                remaining = reader.UnreadSequence;
                position += (int)reader.Consumed;
                previous = current;
                current = token;

                // Capture Line Number
                if (current.TokenType == TokenType.LineFeed && previous.TokenType == TokenType.CarriageReturn)
                {
                    line++;
                }

                switch (token.TokenType)
                {
                    case TokenType.Tab when options.SkipTabs:
                    case TokenType.LineFeed when options.SkipLineFeed:
                    case TokenType.WhiteSpace when options.SkipWhiteSpace:
                    case TokenType.CarriageReturn when options.SkipCarriageReturn:
                    case TokenType.Comment when options.SkipComments:
                        reader = new(remaining); // Reset sequence
                        break;
                    default:
                        return current;
                }
            }
        }

        // If we reached here something is wrong within the syntax
        throw new TokenLexerException("End of Sequence. No more tokens available.");
    }
    /// <summary>
    /// Skip the next token in the collection.
    /// </summary>
    public void Skip()
    {
        Next();
    }
    /// <summary>
    /// Skips a specified number of tokens in the sequence.
    /// </summary>
    /// <param name="count">The number of tokens to skip in the sequence</param>
    public void Skip(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Next();
        }
    }
    /// <summary>
    /// Tries to peek at the next token in the sequence.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public bool TryPeek(out Token token)
    {
        token = default;
        if (HasNext)
        {
            token = Peek();
            return true;
        }
        return false;
    }
    /// <summary>
    /// Tries to retrieve the next token in the sequence.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public bool TryNext(out Token token)
    {
        token = default;
        if (HasNext)
        {
            token = Next();
            return true;
        }
        return false;
    }
    /// <summary>
    /// Gets the raw text within a givven start and end position.
    /// </summary>
    /// <param name="start">The starting position of the data.</param>
    /// <param name="end"></param>
    /// <returns></returns>
    public string GetText(int start, int end)
    {
        var encoding = options.Encoding;
        var span = ValueSpan.Slice(start, end - start);

        return encoding.GetString(span);
    }

    #endregion

    #region Private Methods
    private bool TryParse(ref SequenceReader<byte> sequenceReader, out Token token)
    {
        token = default;
        TokenType tokenType;

        // NOTE: Do not change this order. Conditional evaluation is from left to right on purpose
        if (IsSeparator(ref sequenceReader, out tokenType) ||
            IsKeyword(ref sequenceReader, out tokenType) ||
            IsLiteral(ref sequenceReader, out tokenType) ||
            IsComment(ref sequenceReader, out tokenType) ||
            IsOperator(ref sequenceReader, out tokenType) ||
            IsIdentifier(ref sequenceReader, out tokenType)) // Identifier needs be checked last
        {
            var value = sequenceReader.Slice().ToArray();

            // Temporary Fix: Remove single/double quotes from string
            if (tokenType == TokenType.String)
            {
                value = value.Skip(1).Take(value.Length - 2).ToArray();
            }

            token = new Token()
            {
                Value = value,
                Text = options.Encoding.GetString(value),
                TokenType = tokenType,
                Line = line,
                Start = position, // explicit casting from long to int. If query is bigger than the max value of an int then developer needs to re-think his life decisions.
                End = (int)(position + sequenceReader.Consumed) - 1
            };

            return true;
        }

        return false;
    }
    private bool IsSeparator(ref SequenceReader<byte> sequenceReader, out TokenType tokenType)
    {
        tokenType = default;

        // Separators are one byte long 
        if (sequenceReader.Consumed != 1)
        {
            return false;
        }

        var value = sequenceReader.Slice().ToArray();

        foreach (var separator in TokenSpans.Separators)
        {
            if (separator.Value.SequenceEqual(value))
            {
                tokenType = separator.Key;
                return true;
            }
        }

        return false;
    }
    private bool IsKeyword(ref SequenceReader<byte> sequenceReader, out TokenType tokenType)
    {
        tokenType = default;

        if (sequenceReader.Consumed < 3 || sequenceReader.Consumed > 7)
        {
            return false;
        }

        var value = sequenceReader.SliceToLowerChar().ToArray();

        foreach (var keyword in TokenSpans.Keywords)
        {
            if (keyword.Value.SequenceEqual(value) && (sequenceReader.IsEndOfFileNext() || sequenceReader.IsSeparatorNext()))
            {
                tokenType = keyword.Key;
                return true;
            }
        }

        return false;
    }
    private bool IsOperator(ref SequenceReader<byte> sequenceReader, out TokenType tokenType)
    {
        tokenType = default;
        // No need to check operator since all operators are less than 4 bytes in length
        if (sequenceReader.Consumed > 3)
        {
            return false;
        }

        var value = sequenceReader.SliceToLowerChar().ToArray();

        foreach (var @operator in TokenSpans.Operators)
        {
            if (@operator.Value.SequenceEqual(value) && (sequenceReader.IsEndOfFileNext() || sequenceReader.IsSeparatorNext()))
            {
                tokenType = @operator.Key;
                return true;
            }
        }

        return false;
    }
    private bool IsLiteral(ref SequenceReader<byte> sequenceReader, out TokenType tokenType)
    {
        tokenType = default;

        if (sequenceReader.Consumed == 1)
        {
            // Identify if string literal (single quoted)
            if (sequenceReader.CurrentSpan[0] == (byte)'\'')
            {
                while (sequenceReader.TryRead(out var value))
                {
                    if (value.Equals((byte)'\''))
                    {
                        tokenType = TokenType.String;
                        return true;
                    }
                }
                throw new TokenLexerException("Invalid string format. Missing closing quote.")
                {
                    Position = (int)position
                };
            }
            // Identify if string literal (double quoted)
            if (sequenceReader.CurrentSpan[0] == (byte)'"')
            {
                while (sequenceReader.TryRead(out var value))
                {
                    if (value.Equals((byte)'"'))
                    {
                        tokenType = TokenType.String;
                        return true;
                    }
                }
                throw new TokenLexerException("Invalid string format. Missing closing quote.")
                {
                    Position = (int)position
                };
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
            var split = sequenceReader.IsEndOfFileNext() || sequenceReader.IsSeparatorNext();

            foreach (var literal in TokenSpans.Literals)
            {
                if (literal.Value.SequenceEqual(value) && split)
                {
                    tokenType = literal.Key;
                    return true;
                }
            }
        }

        return false;
    }
    private bool IsComment(ref SequenceReader<byte> sequenceReader, out TokenType tokenType)
    {
        tokenType = default;

        if (sequenceReader.Consumed == 1 && sequenceReader.CurrentSpan[0] == '#')
        {
            tokenType = TokenType.Comment;

            var previous = default(byte);
            var current = default(byte);

            while (sequenceReader.TryRead(out current))
            {
                if (previous == (byte)'\r' && current == '\n')
                {
                    sequenceReader.Rewind(2);
                    break;
                }
                previous = current;
            }

            return true;
        }

        return false;
    }
    private bool IsIdentifier(ref SequenceReader<byte> sequenceReader, out TokenType tokenType)
    {
        tokenType = default;

        // As the lexer loops through the sequence of bytes
        if (sequenceReader.IsSeparatorNext() || sequenceReader.IsEndOfFileNext() || !sequenceReader.IsAlphaNumericCharNext()) // This is to account for any unknown char
        {
            // Let's check if the span starts with a variable identifier
            if (sequenceReader.CurrentSpan[0] == '@')
            {
                tokenType = TokenType.Variable;
                return true;
            }

            tokenType = TokenType.Identifier;
            return true;
        }

        return false;
    }
    #endregion

    #region Static Methods
    public static TokenLexer Create(string query, TokenLexerOptions options)
    {
        if (string.IsNullOrEmpty(query))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(query));
        }

        var bytes = options.Encoding.GetBytes(query);

        return new TokenLexer(
            bytes,
            options);
    }
    #endregion
}