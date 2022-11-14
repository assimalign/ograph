using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

/// <summary>
/// 
/// The Token Lexer (also known as a Tokenizer and)
/// </summary>
public ref partial struct TokenLexer
{
    private ReadOnlySequence<byte> sequence; // Maintain Original Sequence
    private ReadOnlySequence<byte> remaining; //

    private Token current = default;
    private long currentPosition = default;

    public TokenLexer(byte[] query)
    {
        this.sequence = new ReadOnlySequence<byte>(query);
        this.remaining = sequence;
    }

    #region Public Methods
    /// <summary>
    /// Specifies whether the Lexer has another token within the sequence.
    /// </summary>
    public bool HasNext => !remaining.IsEmpty;
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
        var sequenceReader = new SequenceReader<byte>(remaining);

        while (!sequenceReader.End)
        {
            sequenceReader.Advance(1);

            if (TryParse(ref sequenceReader, out var token))
            {
                return token;
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

                current = token;

                return current;
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
        var sequenceSpan = sequenceReader.SliceCurrent();

        return
            IsSeperator(ref sequenceReader, sequenceSpan, out token) ||
            IsOperator(ref sequenceReader, sequenceSpan, out token) ||
            IsLiteral(ref sequenceReader, sequenceSpan, out token) ||
            IsKeyword(ref sequenceReader, sequenceSpan, out token) ||
            IsIdentifier(ref sequenceReader, sequenceSpan, out token); // Identifier needs be checked last
    }
    private bool IsSeperator(ref SequenceReader<byte> sequenceReader, in ReadOnlySpan<byte> sequenceSpan, out Token token)
    {
        // REMINDERS: 
        // - DON'T check for ''' single quotes as these are indicators of a string or a type derived from a string
        token = default;

        if (Tab.SequenceEqual(sequenceSpan))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.Tab);
            return true;
        }
        if (WhiteSpace.SequenceEqual(sequenceSpan))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.WhiteSpace);
            return true;
        }
        if (Dot.SequenceEqual(sequenceSpan))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.Dot);
            return true;
        }
        if (Comma.SequenceEqual(sequenceSpan))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.Comma);
            return true;
        }
        if (OpenParentheses.SequenceEqual(sequenceSpan))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.OpenParenthesis);
            return true;
        }
        if (CloseParentheses.SequenceEqual(sequenceSpan))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.CloseParenthesis);
            return true;
        }
        if (OpenBracket.SequenceEqual(sequenceSpan))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.OpenBracket);
            return true;
        }
        if (CloseBracket.SequenceEqual(sequenceSpan))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.CloseBracket);
            return true;
        }
        if (Colon.SequenceEqual(sequenceSpan))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.Colon);
            return true;
        }
        if (Semicolon.SequenceEqual(sequenceSpan))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.Semicolon);
            return true;
        }
        if (CarriageReturn.SequenceEqual(sequenceSpan))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.CarriageReturn);
            return true;
        }
        if (NewLine.SequenceEqual(sequenceSpan))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.NewLine);
            return true;
        }

        return false;
    }
    private bool IsKeyword(ref SequenceReader<byte> sequenceReader, in ReadOnlySpan<byte> sequenceSpan, out Token token)
    {
        token = default;

        if (FilterClause.SequenceEqual(sequenceSpan) && !sequenceReader.IsAlphaNumericCharNext())
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.Filter);
            return true;
        }
        if (SelectClause.SequenceEqual(sequenceSpan) && !sequenceReader.IsAlphaNumericCharNext())
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.Select);
            return true;
        }
        if (SortClause.SequenceEqual(sequenceSpan) && !sequenceReader.IsAlphaNumericCharNext())
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.Sort);
            return true;
        }
        if (PageClause.SequenceEqual(sequenceSpan) && !sequenceReader.IsAlphaNumericCharNext())
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.Page);
            return true;
        }
        if (AsClause.SequenceEqual(sequenceSpan) && !sequenceReader.IsAlphaNumericCharNext())
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.Alias);
            return true;
        }

        return false;
    }
    private bool IsLiteral(ref SequenceReader<byte> sequenceReader, in ReadOnlySpan<byte> sequenceSpan, out Token token)
    {
        token = default;

        var value = GetCurrentSpan(ref sequenceReader);


        // Identify whether current span starts with a string literal
        if (SingleQuoteOperator.SequenceEqual(value))
        {
            if (!sequenceReader.TryAdvanceTo((byte)'\''))
            {
                throw new Exception("Invalid string literal");
            }

            value = GetCurrentSpan(ref sequenceReader);

            token = new Token()
            {
                Value = value.Slice(1, value.Length - 2).ToArray(),
                TokenType = TokenType.String
            };

            return true;
        }


        // Check if the current span in the seqnece reader is one and it starts with a string literal single qupte
        if (value.Length == 1 && SingleQuoteOperator.SequenceEqual(value))
        {
            // Try to go to next 
            if (!sequenceReader.TryAdvanceTo((byte)'\''))
            {

            }

            var stringLiteral = GetCurrentSpan(ref sequenceReader);
        }
        if ((BooleanFalseLiteral.SequenceEqual(value) || 
            BooleanTrueLiteral.SequenceEqual(value)) &&
            (sequenceReader.IsNext(WhiteSpace) ||
            sequenceReader.IsNext(Tab) ||
            sequenceReader.IsNext(CarriageReturn) ||
            sequenceReader.IsNext(NewLine)))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.Boolean);
            return true;
        }

        return false;
    }
    private bool IsOperator(ref SequenceReader<byte> sequenceReader, in ReadOnlySpan<byte> sequenceSpan, out Token token)
    {
        token = default;

        // *** An operator should always have an exact sequence separated by a WhiteSpace in between.
        // Let's ensure we are not just reading the start of an identifier
        if (EqualOperator.SequenceEqual(sequenceSpan) && sequenceReader.IsNext(WhiteSpace))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.Equal);
            return true;
        }
        if (NotEqualOperator.SequenceEqual(sequenceSpan) && sequenceReader.IsNext(WhiteSpace))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.NotEqual);
            return true;
        }
        if (GreaterThanOperator.SequenceEqual(sequenceSpan) && sequenceReader.IsNext(WhiteSpace))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.GreaterThan);
            return true;
        }
        if (GreaterThanOrEqualOperator.SequenceEqual(sequenceSpan) && sequenceReader.IsNext(WhiteSpace))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.GreaterThanOrEqual);
            return true;
        }
        if (LessThanOperator.SequenceEqual(sequenceSpan) && sequenceReader.IsNext(WhiteSpace))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.LessThan);
            return true;
        }
        if (LessThanOrEqualOperator.SequenceEqual(sequenceSpan) && sequenceReader.IsNext(WhiteSpace))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.LessThanOrEqual);
            return true;
        }
        if (AndOperator.SequenceEqual(sequenceSpan) && 
            (sequenceReader.IsNext(WhiteSpace) ||
            sequenceReader.IsNext(Tab) ||
            sequenceReader.IsNext(CarriageReturn) ||
            sequenceReader.IsNext(NewLine)))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.And);
            return true;
        }
        if (OrOperator.SequenceEqual(sequenceSpan) && 
            (sequenceReader.IsNext(WhiteSpace) ||
            sequenceReader.IsNext(Tab) ||
            sequenceReader.IsNext(CarriageReturn) ||
            sequenceReader.IsNext(NewLine)))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.Or); 
            return true;
        }

        return false;
    }
    private bool IsIdentifier(ref SequenceReader<byte> sequenceReader, in ReadOnlySpan<byte> sequenceSpan, out Token token)
    {
        token = default;

        if (sequenceReader.TryPeek(out var b) && !char.IsLetter((char)b))
        {
            token = GetToken(sequenceReader, sequenceSpan, TokenType.Identifier);

            return true;
        }

        return false;
    }


    private Token GetToken(in SequenceReader<byte> sequenceReader, in ReadOnlySpan<byte> sequenceSpan, TokenType tokenType)
    {
        return new Token()
        {
            Value = sequenceSpan.ToArray(),
            TokenType = tokenType,
            Start = currentPosition,
            End = (currentPosition += sequenceReader.Consumed) - 1
        };
    }

    private ReadOnlySpan<byte> GetCurrentSpan(ref SequenceReader<byte> sequenceReader)
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