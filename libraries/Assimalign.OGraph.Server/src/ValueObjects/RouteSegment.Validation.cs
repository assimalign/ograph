using System;
using System.Text;
using System.Buffers;
using System.Collections.Generic;

namespace Assimalign.OGraph;

public readonly partial struct RouteSegment
{
    //delegate bool Validator(object value, out string error);

    //private readonly List<Validator> validators = new List<Validator>();

    //public void Temp()
    //{
    //    var bytes = Encoding.UTF8.GetBytes(Value);
    //    var sequence = new ReadOnlySequence<byte>(bytes);
    //    var reader = new SequenceReader<byte>(sequence);
    //    var delimiter = new byte[] { (byte)':' };

    //    ReadOnlySequence<byte> current;

    //    while (reader.TryRead(out var b))
    //    {
    //        if (b == (byte)'{') continue;   // Skip open bracket
    //        if (b == (byte)'}') break;      // Stop parsing

    //        if (reader.TryReadTo(out current, delimiter, false))
    //        {
    //            Parse(ref current);
    //        }
    //    }
    //}


    ///// <summary>
    ///// 
    ///// </summary>
    ///// <returns></returns>
    //public string? GetParamName()
    //{

    //}

    ///// <summary>
    ///// Validates path segment
    ///// </summary>
    ///// <param name="segment"></param>
    ///// <param name="error"></param>
    ///// <returns></returns>
    //public bool IsValid(PathSegment segment, out string? error)
    //{
    //    error = null;

    //    var value = segment.Value;

    //    var bytes = Encoding.UTF8.GetBytes(Value);
    //    var sequence = new ReadOnlySequence<byte>(bytes);
    //    var reader = new SequenceReader<byte>(sequence);
    //    var delimiter = new byte[] { (byte)':' };

    //    ReadOnlySequence<byte> current;

    //    while (reader.TryRead(out var b))
    //    {
    //        if (b == (byte)'{') continue;   // Skip open bracket
    //        if (b == (byte)'}') break;      // Stop parsing

    //        if (reader.TryReadTo(out current, delimiter, false))
    //        {
    //            Parse(ref current);
    //        }
    //    }

    //    return false;
    //}

    //private static void Parse(ref ReadOnlySequence<byte> sequence)
    //{

    //}


    //private static bool IsInt32(string value, out int number)
    //{
    //    return int.TryParse(value, out number);
    //}
}
