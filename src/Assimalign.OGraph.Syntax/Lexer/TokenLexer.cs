using System;
using System.Buffers;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

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
        throw new Exception("End of Sequence");
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
        throw new Exception("End of Sequence");
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
        if (sequenceReader.IsSeparator(out tokenType) ||
            sequenceReader.IsKeyword(out tokenType)   ||
            sequenceReader.IsLiteral(out tokenType)   ||
            sequenceReader.IsComment(out tokenType)   ||
            sequenceReader.IsOperator(out tokenType)  ||
            sequenceReader.IsIdentifer(out tokenType)) // Identifier needs be checked last
        {
            token = new Token()
            {
                Value = sequenceReader.Slice().ToArray(),
                TokenType = tokenType,
                Start = (int)position,
                End = (int)(position + sequenceReader.Consumed) - 1
            };
            return true;
        }

        return false;
    }
    #endregion
}