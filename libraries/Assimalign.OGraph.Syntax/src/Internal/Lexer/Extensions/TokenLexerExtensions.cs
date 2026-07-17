using System;
using System.Linq;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Assimalign.OGraph.Syntax.Internal;

internal static class TokenLexerExtensions
{

    // Checks if the current consumed byte equals the given value
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool ByteEquals(this ref SequenceReader<byte> sequenceReader, byte value)
    {
        return sequenceReader.CurrentSpan[(int)sequenceReader.Consumed] == value;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsSeparatorNext(this ref SequenceReader<byte> sequenceReader)
    {
        if (sequenceReader.TryPeek(out var value))
        {
            var array = new byte[] { value };

            foreach (var separator in TokenSpans.Separators)
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
    internal static bool IsAlphaNumericCharNext(this ref SequenceReader<byte> sequenceReader) => sequenceReader.TryPeek(out var value) && char.IsLetterOrDigit((char)value);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsEndOfFileNext(this ref SequenceReader<byte> sequenceReader) => sequenceReader.Remaining <= 0;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static ReadOnlySpan<byte> Slice(this ref SequenceReader<byte> sequenceReader)
    {
        return sequenceReader.CurrentSpan.Slice(0, (int)sequenceReader.Consumed);
    }


    // Slices current consumed sequence as span and convert all alpha-characters to lower case if not already
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static ReadOnlySpan<byte> SliceToLowerChar(this ref SequenceReader<byte> sequenceReader)
    {
        var buffer = new byte[sequenceReader.Consumed];

        for (int i = 0; i < sequenceReader.Consumed; i++)
        {
            buffer[i] = (byte)char.ToLower((char)sequenceReader.CurrentSpan[i]);
        }

        return buffer.AsSpan();
    }

}