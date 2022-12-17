using System;
using System.Linq;
using System.Buffers;

namespace Assimalign.OGraph.Syntax.Lexer;

using Assimalign.OGraph.Syntax.Lexer.Internal;


/// <summary>
/// The Token Lexer (also known as a Tokenizer and)
/// </summary>
public ref partial struct TokenLexer
{
    private readonly TokenLexerOptions options;

    private ReadOnlySequence<byte> remaining;
    private Token current = default;
    private long position = default;

    public TokenLexer(byte[] query) : this(query, new()) { }
    public TokenLexer(byte[] query, TokenLexerOptions options)
    {
        this.options = options;
        this.remaining = new ReadOnlySequence<byte>(query); ;
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
                        case TokenType.Tab              when options.SkipTabs:
                        case TokenType.LineFeed         when options.SkipLineFeed:
                        case TokenType.WhiteSpace       when options.SkipWhiteSpace:
                        case TokenType.CarriageReturn   when options.SkipCarriageReturn:
                        case TokenType.Comment          when options.SkipComments:
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
    /// Is the current token that has been parsed.
    /// </summary>
    public Token Current => this.current;
    /// <summary>
    /// Peeks at the next token in the sequence.
    /// </summary>
    /// <returns></returns>
    public Token Peek()
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
    /// <exception cref="Exception"></exception>
    public Token Next()
    {
        var sequenceReader = new SequenceReader<byte>(remaining);

        while (!sequenceReader.End)
        {
            sequenceReader.Advance(1);

            if (TryParse(ref sequenceReader, out var token))
            {
                remaining = sequenceReader.UnreadSequence;
                position += sequenceReader.Consumed;
                current = token;

                switch (token.TokenType)
                {
                    case TokenType.Tab when options.SkipTabs:
                    case TokenType.LineFeed when options.SkipLineFeed:
                    case TokenType.WhiteSpace when options.SkipWhiteSpace:
                    case TokenType.CarriageReturn when options.SkipCarriageReturn:
                    case TokenType.Comment when options.SkipComments:
                        sequenceReader = new(remaining); // Reset sequence
                        break;
                    default:
                        return current;
                }
            }
        }

        // If we reached here something is wrong within the syntax
        throw new TokenLexerException("End of Sequence. No more tokens available.");
    }

    public void Skip()
    {
        Next();
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
    #endregion

    #region Private Methods
    private bool TryParse(ref SequenceReader<byte> sequenceReader, out Token token)
    {
        token = default;
        TokenType tokenType;

        // NOTE: Do not change this order. Conditional evaluation is from left to right on purpose
        if (IsSeparator(ref sequenceReader, out tokenType)  ||
            IsKeyword(ref sequenceReader, out tokenType)    ||
            IsLiteral(ref sequenceReader, out tokenType)    ||
            IsComment(ref sequenceReader, out tokenType)    ||
            IsOperator(ref sequenceReader, out tokenType)   ||
            IsIdentifer(ref sequenceReader, out tokenType)) // Identifier needs be checked last
        {
            token = new Token()
            {
                Value = sequenceReader.Slice().ToArray(),
                TokenType = tokenType,
                Start = (int)position, // explicit casting from long to int. If query is bigger than the max value of an int then developer needs to re-think his life decisions.
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
            // Identify if string literal
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

            foreach (var literal in TokenSpans.Literals)
            {
                if (literal.Value.SequenceEqual(value) && (sequenceReader.IsEndOfFileNext() || sequenceReader.IsSeparatorNext()))
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
    private bool IsIdentifer(ref SequenceReader<byte> sequenceReader, out TokenType tokenType)
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
}