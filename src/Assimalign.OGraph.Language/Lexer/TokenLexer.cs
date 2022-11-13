using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;

public ref partial struct TokenLexer
{
    private ReadOnlySequence<byte> sequence; // Maintain Original Sequence
    private ReadOnlySequence<byte> remaining; //

    private Token currentToken = default;
    private long currentPosition = default;


    public TokenLexer(byte[] query)
    {
        this.sequence = new ReadOnlySequence<byte>(query);
        this.remaining = sequence;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool HasNext => !remaining.IsEmpty;
    /// <summary>
    /// 
    /// </summary>
    public Token CurrentToken => this.currentToken;
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Token Peek()
    {
        //		var previousPosition = sequenceReader.Consumed;
        //
        //		var token = Next();
        //
        //		var currentPosition = sequenceReader.Consumed;
        //
        //		sequenceReader.Rewind(currentPosition - previousPosition);

        return default;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Token Next()
    {
        if (currentToken.TokenType != TokenType.End)
        {
            var sequenceReader = new SequenceReader<byte>(remaining);
            while (!sequenceReader.End)
            {
                sequenceReader.Advance(1);
                if (TryParse(ref sequenceReader, out var token))
                {
                    remaining = sequenceReader.UnreadSequence;
                    currentToken = token;
                    return currentToken;
                }
            }
        }
        // If we reached here something is wrong within the syntax
        throw new Exception("End of Sequence");
    }

    /*
		When parsing tokens considerations of higher and lower precedence is important
		
		string literal
			-> Check is DateTime Literal
			-> Check is DateOnly Literal
			
			-> else escape and return String Literal
	
	*/

    private bool TryParse(ref SequenceReader<byte> sequenceReader, out Token token)
    {
        var sequenceSpan = sequenceReader.SliceCurrent();

        return
            IsSeperator(ref sequenceReader, sequenceSpan, out token) ||
            IsOperator(ref sequenceReader, out token) ||
            IsLiteral(ref sequenceReader, out token) ||
            IsKeyword(ref sequenceReader, sequenceSpan, out token) ||	
            IsIdentifier(ref sequenceReader, out token); // Identifier needs be checked last
    }
    private bool IsSeperator(ref SequenceReader<byte> sequenceReader, in ReadOnlySpan<byte> sequenceSpan, out Token token)
    {
        // REMINDERS: 
        // - DON'T check for ''' single quotes as these are indicators of a string or a type derived from a string
        token = default;

        if (Tab.SequenceEqual(sequenceSpan))
        {
            token = new Token()
            {
                RValue = (char)sequenceSpan[0],
                Value = sequenceSpan.ToArray(),
                TokenType = TokenType.Tab,
                Start = currentPosition,
                End = (currentPosition += sequenceReader.Consumed) - 1
            };
            return true;
        }
        if (WhiteSpace.SequenceEqual(sequenceSpan))
        {
            token = new Token()
            {
                RValue = (char)sequenceSpan[0],
                Value = sequenceSpan.ToArray(),
                TokenType = TokenType.WhiteSpace
            };
            return true;
        }
        if (Dot.SequenceEqual(sequenceSpan))
        {
            token = new Token()
            {
                RValue = (char)sequenceSpan[0],
                Value = sequenceSpan.ToArray(),
                TokenType = TokenType.Dot
            };
            return true;
        }
        if (Comma.SequenceEqual(sequenceSpan))
        {
            token = new Token()
            {
                RValue = (char)sequenceSpan[0],
                Value = sequenceSpan.ToArray(),
                TokenType = TokenType.Comma
            };
            return true;
        }
        if (OpenParentheses.SequenceEqual(sequenceSpan))
        {
            token = new Token()
            {
                RValue = (char)sequenceSpan[0],
                Value = sequenceSpan.ToArray(),
                TokenType = TokenType.OpenParenthesis
            };
            return true;
        }
        if (CloseParentheses.SequenceEqual(sequenceSpan))
        {
            token = new Token()
            {
                RValue = (char)sequenceSpan[0],
                Value = sequenceSpan.ToArray(),
                TokenType = TokenType.CloseParenthesis
            };
            return true;
        }
        if (OpenBracket.SequenceEqual(sequenceSpan))
        {
            token = new Token()
            {
                RValue = (char)sequenceSpan[0],
                Value = sequenceSpan.ToArray(),
                TokenType = TokenType.OpenBracket
            };
            return true;
        }
        if (CloseBracket.SequenceEqual(sequenceSpan))
        {
            token = new Token()
            {
                RValue = (char)sequenceSpan[0],
                Value = sequenceSpan.ToArray(),
                TokenType = TokenType.CloseBracket
            };
            return true;
        }
        if (Colon.SequenceEqual(sequenceSpan))
        {
            token = new Token()
            {
                RValue = (char)sequenceSpan[0],
                Value = sequenceSpan.ToArray(),
                TokenType = TokenType.Colon
            };
            return true;
        }
        if (Semicolon.SequenceEqual(sequenceSpan))
        {
            token = new Token()
            {
                RValue = (char)sequenceSpan[0],
                Value = sequenceSpan.ToArray(),
                TokenType = TokenType.Semicolon
            };
            return true;
        }
        if (CarriageReturn.SequenceEqual(sequenceSpan))
        {
            token = new Token()
            {
                RValue = (char)sequenceSpan[0],
                Value = sequenceSpan.ToArray(),
                TokenType = TokenType.CarriageReturn
            };
            return true;
        }
        if (NewLine.SequenceEqual(sequenceSpan))
        {
            token = new Token()
            {
                RValue = (char)sequenceSpan[0],
                Value = sequenceSpan.ToArray(),
                TokenType = TokenType.NewLine
            };
            return true;
        }

        return false;
    }
    private bool IsKeyword(ref SequenceReader<byte> sequenceReader, in ReadOnlySpan<byte> sequenceSpan, out Token token)
    {
        token = default;

        if (FilterClause.SequenceEqual(sequenceSpan))
        {
            token = new Token()
            {
                Value = sequenceSpan.ToArray(),
                TokenType = TokenType.Filter
            };
            return true;
        }
        if (SelectClause.SequenceEqual(sequenceSpan))
        {
            token = new Token()
            {
                Value = sequenceSpan.ToArray(),
                TokenType = TokenType.Filter
            };
            return true;
        }
        if (SortClause.SequenceEqual(sequenceSpan))
        {
            token = new Token()
            {
                RValue = Encoding.UTF8.GetString(sequenceSpan),
                Value = sequenceSpan.ToArray(),
                TokenType = TokenType.Sort
            };
            return true;
        }
        if (PageClause.SequenceEqual(sequenceSpan))
        {
            token = new Token()
            {
                RValue = Encoding.UTF8.GetString(sequenceSpan),
                Value = sequenceSpan.ToArray(),
                TokenType = TokenType.Page
            };
            return true;
        }
        if (AsClause.SequenceEqual(sequenceSpan) &&
            (sequenceReader.IsNext(WhiteSpace) ||
            sequenceReader.IsNext(Tab) ||
            sequenceReader.IsNext(CarriageReturn) ||
            sequenceReader.IsNext(NewLine)))
        {
            token = new Token()
            {
                RValue = Encoding.UTF8.GetString(sequenceSpan),
                Value = sequenceSpan.ToArray(),
                TokenType = TokenType.Alias
            };
            return true;
        }

        return false;
    }
    private bool IsLiteral(ref SequenceReader<byte> sequenceReader, out Token token)
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
        if (BooleanFalseLiteral.SequenceEqual(value) || BooleanTrueLiteral.SequenceEqual(value))
        {
            token = new Token()
            {
                RValue = bool.Parse(Encoding.UTF8.GetString(value)),
                Value = value.ToArray(),
                TokenType = TokenType.Boolean
            };
            return true;
        }

        return false;
    }
    private bool IsOperator(ref SequenceReader<byte> sequenceReader, out Token token)
    {
        token = default;

        var value = GetCurrentSpan(ref sequenceReader);
        var isWhiteSpaceNext = sequenceReader.IsNext(WhiteSpace);

        // *** An operator should always have an exact sequence separated by a WhiteSpace in between.
        // Let's ensure we are not just reading the start of an identifier
        if (EqualOperator.SequenceEqual(value) && isWhiteSpaceNext)
        {
            token = new Token()
            {
                RValue = Encoding.UTF8.GetString(value),
                Value = value.ToArray(),
                TokenType = TokenType.Equal
            };

            return true;
        }
        if (NotEqualOperator.SequenceEqual(value) && isWhiteSpaceNext)
        {
            token = new Token()
            {
                RValue = Encoding.UTF8.GetString(value),
                Value = value.ToArray(),
                TokenType = TokenType.NotEqual
            };

            return true;
        }
        if (GreaterThanOperator.SequenceEqual(value) && isWhiteSpaceNext)
        {
            token = new Token()
            {
                RValue = Encoding.UTF8.GetString(value),
                Value = value.ToArray(),
                TokenType = TokenType.GreaterThan
            };

            return true;
        }
        if (GreaterThanOrEqualOperator.SequenceEqual(value) && isWhiteSpaceNext)
        {
            token = new Token()
            {
                RValue = Encoding.UTF8.GetString(value),
                Value = value.ToArray(),
                TokenType = TokenType.GreaterThanOrEqual
            };

            return true;
        }
        if (LessThanOperator.SequenceEqual(value) && isWhiteSpaceNext)
        {
            token = new Token()
            {
                RValue = Encoding.UTF8.GetString(value),
                Value = value.ToArray(),
                TokenType = TokenType.LessThan
            };

            return true;
        }
        if (LessThanOrEqualOperator.SequenceEqual(value) && isWhiteSpaceNext)
        {
            token = new Token()
            {
                RValue = Encoding.UTF8.GetString(value),
                Value = value.ToArray(),
                TokenType = TokenType.LessThanOrEqual
            };

            return true;
        }
        if (AndOperator.SequenceEqual(value) && (sequenceReader.IsNext(WhiteSpace) ||
            sequenceReader.IsNext(Tab) ||
            sequenceReader.IsNext(CarriageReturn) ||
            sequenceReader.IsNext(NewLine)))
        {
            token = new Token()
            {
                RValue = Encoding.UTF8.GetString(value),
                Value = value.ToArray(),
                TokenType = TokenType.And
            };

            return true;
        }
        if (OrOperator.SequenceEqual(value) && (sequenceReader.IsNext(WhiteSpace) ||
            sequenceReader.IsNext(Tab) ||
            sequenceReader.IsNext(CarriageReturn) ||
            sequenceReader.IsNext(NewLine)))
        {
            token = new Token()
            {
                RValue = Encoding.UTF8.GetString(value),
                Value = value.ToArray(),
                TokenType = TokenType.Or
            };

            return true;
        }

        return false;
    }
    
    // Identifiers are entity types suach as properties, fields, and functions
    private bool IsIdentifier(ref SequenceReader<byte> sequenceReader, out Token token)
    {
        token = default;

        var slice = sequenceReader.SliceCurrent();

        var value = GetCurrentSpan(ref sequenceReader);

        if (sequenceReader.TryPeek(out var b) && !char.IsLetter((char)b))
        {
            token = new Token()
            {
                RValue = Encoding.UTF8.GetString(value),
                Value = value.ToArray(),
                TokenType = TokenType.Identifier
            };

            return true;
        }

        return false;
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

    #region Literal Helpers
    private bool IsStringLiteralDateTime(string value)
    {
        return true;
    }

    #endregion
}
