using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace Assimalign.OGraph;

[DebuggerDisplay("Segment: {Value}")]
public readonly struct RouteSegment :
    IEquatable<RouteSegment>,
    IEqualityComparer<RouteSegment>
{
    internal RouteSegment(string value, int ordinal)
    {
        if (value[0] == '{' && value[value.Length - 1] == '}')
        {
            var item = value.Substring(1, value.Length - 2);

            this.Value = value;
            this.SegmentType = RouteSegmentType.Parameter;
        }
        else
        {
            this.Value = value;
            this.SegmentType = RouteSegmentType.Literal;
        }
        this.Ordinal = ordinal;
    }

    /// <summary>
    /// The raw segment value.
    /// </summary>
    public string Value { get; }
    /// <summary>
    /// The segment type.
    /// </summary>
    public RouteSegmentType SegmentType { get; }
    /// <summary>
    /// The index of the route segment.
    /// </summary>
    public int Ordinal { get; }
    /// <summary>
    /// 
    /// </summary>
    public bool Equals(RouteSegment other)
    {
        return Ordinal == other.Ordinal && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
    }
    /// <summary>
    /// 
    /// </summary>
    public bool Equals(RouteSegment right, RouteSegment left)
    {
        return right.Equals(left);
    }
    /// <summary>
    /// 
    /// </summary>
    public int GetHashCode(RouteSegment instance)
    {
        return instance.GetHashCode();
    }
    
    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? instance)
    {
        if (instance is RouteSegment segment)
        {
            return Equals(segment);
        }
        return false;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(RouteSegment), Ordinal, string.Create(Value.Length, Value, (chars, name) =>
        {
            // A string value's hashcode takes casing into account. Let's lowercase the characters since routes are insensitive
            name.CopyTo(chars);
            for (int i = 0; i < chars.Length;i++)
            {
                var c = chars[i];
                if (char.IsUpper(c))
                {
                    chars[i] = char.ToLower(c);
                }
            }
        }));
    }

    /// <inheritdoc />
    public override string ToString() => Value;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="paramName"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public T FormatValue<T>(string value) where T : struct 
    {
        if (SegmentType.Equals(RouteSegmentType.Literal))
        {
            throw new InvalidOperationException("FormatValue<> is not allowed on Literal Segment Types.");
        }
        if (!parsers.TryGetValue(typeof(T), out var parser))
        {
            throw new InvalidOperationException($"{typeof(T).Name} is not supported for casting.");
        }
        return (T)parser.Invoke(value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="segment"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public bool IsMatch(PathSegment segment)
    {
        switch (SegmentType)
        {
            case RouteSegmentType.Literal:
                {
                    return Ordinal == segment.Ordinal && Value.Equals(segment.Value, StringComparison.OrdinalIgnoreCase);
                }
            case RouteSegmentType.Parameter:
                {
                    if (Value.IndexOf(':')  != -1)
                    {

                    }

                    return Ordinal == segment.Ordinal;
                }
            default:
                {
                    throw new Exception();
                }
        }
    }


    private static IDictionary<Type, Func<string, object>> parsers => new Dictionary<Type, Func<string, object>>()
    {
        {typeof(short), value => short.Parse(value)},
        {typeof(short?), value => new Nullable<short>(short.Parse(value))},

        {typeof(int), value => int.Parse(value)},
        {typeof(int?), value => new Nullable<int>(int.Parse(value))},

        {typeof(long), value => long.Parse(value)},
        {typeof(long?), value => new Nullable<long>(long.Parse(value))},

        {typeof(ushort), value => ushort.Parse(value)},
        {typeof(ushort?), value => new Nullable<ushort>(ushort.Parse(value))},

        {typeof(uint), value => int.Parse(value)},
        {typeof(uint?), value => new Nullable<uint>(uint.Parse(value))},

        {typeof(ulong), value => long.Parse(value)},
        {typeof(ulong?), value => new Nullable<ulong>(ulong.Parse(value))},

        {typeof(float), value => float.Parse(value)},
        {typeof(float?), value => new Nullable<float>(float.Parse(value))},

        {typeof(double), value => double.Parse(value)},
        {typeof(double?), value => new Nullable<double>(double.Parse(value))},

        {typeof(decimal), value => decimal.Parse(value)},
        {typeof(decimal?), value => new Nullable<decimal>(decimal.Parse(value))},

        {typeof(TimeSpan), value => TimeSpan.Parse(value)},
        {typeof(TimeSpan?), value => new Nullable<TimeSpan>(TimeSpan.Parse(value))},

        {typeof(DateTime), value => DateTime.Parse(value)},
        {typeof(DateTime?), value => new Nullable<DateTime>(DateTime.Parse(value))},

        {typeof(DateTimeOffset), value => DateTimeOffset.Parse(value)},
        {typeof(DateTimeOffset?), value => new Nullable<DateTimeOffset>(DateTimeOffset.Parse(value))},

        {typeof(DateOnly), value => DateOnly.Parse(value)},
        {typeof(DateOnly?), value => new Nullable<DateOnly>(DateOnly.Parse(value))},

        {typeof(TimeOnly), value => TimeOnly.Parse(value)},
        {typeof(TimeOnly?), value => new Nullable<TimeOnly>(TimeOnly.Parse(value))},

        {typeof(Guid), value => Guid.Parse(value)},
        {typeof(Guid?), value => new Nullable<Guid>(Guid.Parse(value))},
    };
}