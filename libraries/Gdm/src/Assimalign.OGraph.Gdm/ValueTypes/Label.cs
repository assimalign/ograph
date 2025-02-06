using System;
using System.Globalization;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Assimalign.OGraph;

using Gdm;
using Gdm.Internal;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// Allowed values: 
/// <list type="bullet">
/// <item>Arabic Letters: a-z, A-Z</item>
/// <item>Numeric Values: 0-9</item>
/// <item>Special Characters: - _ @</item>
/// </list>
/// </remarks>
[DebuggerDisplay("{Value}")]
[TypeConverter(typeof(LabelTypeConverter))]
public readonly struct Label : 
    IEquatable<Label>, 
    IEqualityComparer<Label>,
    IComparable<Label>
{
    // Allowed characters for name
    private const string pattern = "^[a-zA-Z0-9_@-]+$";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="GdmException"></exception>
    public Label(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(value));
        }
        if (!Regex.IsMatch(value, pattern))
        {
            ThrowHelper.ThrowInvalidLabel(value);
        }
        Value = value;
    }

    /// <summary>
    /// The raw name value.
    /// </summary>
    public readonly string Value { get; }

    /// <summary>
    /// Converts the string to pascal case.
    /// </summary>
    /// <returns></returns>
    public Label ToPascalCase()
    {
        return AsPascalCase(Value);
    }

    /// <summary>
    /// Converts the name to camal case
    /// </summary>
    /// <returns></returns>
    public Label ToCamalCase()
    {
        return AsCamalCase(Value);
    }

    /// <summary>
    /// Creates a label by converting the the string value to camal case.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Label AsCamalCase(string value)
    {
        return string.Create(value.Length, value, (chars, name) =>
        {
            name.CopyTo(chars);

            for (int i = 0; i < chars.Length && (i != 1 || char.IsUpper(chars[i])); i++)
            {
                bool flag = i + 1 < chars.Length;
                if (i > 0 && flag && !char.IsUpper(chars[i + 1]))
                {
                    if (chars[i + 1] == ' ')
                    {
                        chars[i] = char.ToLowerInvariant(chars[i]);
                    }
                    break;
                }
                chars[i] = char.ToLowerInvariant(chars[i]);
            }
        });
    }

    /// <summary>
    ///  Creates a label by converting the the string value to pascal case.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Label AsPascalCase(string value)
    {
        return string.Create(value.Length, value, (chars, name) =>
        {
            name.CopyTo(chars);

            for (int i = 0; i < chars.Length && (i != 1 || char.IsUpper(chars[i])); i++)
            {
                bool flag = i + 1 < chars.Length;
                if (i > 0 && flag && !char.IsUpper(chars[i + 1]))
                {
                    if (chars[i + 1] == ' ')
                    {
                        chars[i] = char.ToUpperInvariant(chars[i]);
                    }
                    break;
                }
                chars[i] = char.ToUpperInvariant(chars[i]);
            }
        });
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return Value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }
        return obj is Label other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        // TODO: Need to revisit. Not sure if I want the HashCode for the name to be the same as the instance of the string.
        return Value.GetHashCode();
    }

    /// <inheritdoc />
    public bool Equals(Label left, Label right)
    {
        return left.Equals(right);
    }

    /// <inheritdoc />
    public int GetHashCode([DisallowNull] Label name)
    {
        return name.GetHashCode();
    }


    /// <inheritdoc cref="global::System.IEquatable{T}"/>
    public bool Equals(Label other)
    {
        return (Value, other.Value) switch
        {
            (null, null) => true,
            (null, _) => false,
            (_, null) => false,
            (_, _) => Value.Equals(other.Value, StringComparison.Ordinal),
        };
    }

    /// <inheritdoc cref="global::System.IComparable{TSelf}"/>
    public int CompareTo(Label other)
    {
        return (Value, other.Value) switch
        {
            (null, null) => 0,
            (null, _) => -1,
            (_, null) => 1,
            (_, _) => string.CompareOrdinal(Value, other.Value),
        };
    }

    public static implicit operator Label(string value) => new Label(value);

    public static implicit operator string(Label name) => name.Value;
    public static bool operator ==(Label left, Label right) => left.Equals(right);
    public static bool operator !=(Label left, Label right) => !left.Equals(right);
    public static bool operator <(Label left, Label right) => left.CompareTo(right) < 0;
    public static bool operator >(Label left, Label right) => left.CompareTo(right) > 0;
    public static bool operator <=(Label left, Label right) => left.CompareTo(right) <= 0;
    public static bool operator >=(Label left, Label right) => left.CompareTo(right) >= 0;


    /// <summary>
    /// Specifies whether the <paramref name="name"/> is a valid name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool IsValid(string name) => Regex.IsMatch(name, pattern);

    internal partial class LabelTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is string stringValue)
            {
                return new Label(stringValue);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext? context, Type? sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertTo(context, sourceType);
        }

        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (value is Label idValue)
            {
                if (destinationType == typeof(string))
                {
                    return idValue.Value;
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}