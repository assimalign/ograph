using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal static class SequenceReaderExtensions
{
    /// <summary>
    /// Slices current consumed sequence as span and convert all alpha-characters 
    /// to lower-case to ensure case-insensitive comparison.
    /// </summary>
    /// <param name="sequenceReader"></param>
    /// <returns></returns>
    public static ReadOnlySpan<byte> SliceCurrent(this ref SequenceReader<byte> sequenceReader)
    {
        var buffer = new byte[sequenceReader.Consumed];

        for (int i = 0; i < sequenceReader.Consumed; i++)
        {
            buffer[i] = (byte)(char.ToLower((char)sequenceReader.CurrentSpan[i]));
        }

        return buffer.AsSpan();
    }

    public static bool IsAlphaNumericCharNext(this ref SequenceReader<byte> sequenceReader)
    {
        if (sequenceReader.TryPeek(out var value))
        {
            return char.IsLetterOrDigit((char)value);
        }

        return false;
    }

    /// <summary>
    /// Specifies whether the next byte in the sequence is a character within the Greek Alphabet.
    /// </summary>
    /// <param name="sequenceReader"></param>
    /// <returns></returns>
    public static bool IsAlphaCharNext(this ref SequenceReader<byte> sequenceReader)
    {
        if (sequenceReader.TryPeek(out var value))
        {
            return char.IsLetter((char)value);
        }

        return false;
    }
    public static bool IsNumericCharNext(this ref SequenceReader<byte> sequenceReader)
    {
        if (sequenceReader.TryPeek(out var value))
        {
            return char.IsDigit((char)value);
        }

        return false;
    }
}
