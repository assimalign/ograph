using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph;

public readonly struct Name : 
    IEquatable<Name>, 
    IEqualityComparer<Name>,
    IComparable<Name>
{
    private const string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567980_";

    public Name(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }
        foreach (var character in value)
        {
            if (!allowedCharacters.Contains(character))
            {
                throw new ArgumentException($"The following name: '{value}' contains invalid characters. Only the following characters are allowed: '{allowedCharacters}'");
            }
        }

        this.Value = value;
    }

    /// <summary>
    /// The raw name value.
    /// </summary>
    public string Value { get; }

    public override string ToString()
    {
        return Value;
    }

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? instance)
    {
        if (instance is not Name name)
        {
            return false;
        }

        return this.Equals(name);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    /// <inheritdoc />
    public bool Equals(Name name)
    {
        return this.Value.Equals(name.Value, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <inheritdoc />
    public bool Equals(Name left, Name right)
    {
        return left.Equals(right);
    }

    /// <inheritdoc />
    public int GetHashCode([DisallowNull] Name name)
    {
        return name.GetHashCode();
    }

    /// <inheritdoc />
    public int CompareTo(Name name)
    {
        return this.Value.ToLowerInvariant().CompareTo(name.Value.ToLowerInvariant());
    }

    public static implicit operator Name(string value) => new Name(value);
    public static implicit operator string(Name name) => name.Value;



    public static bool operator ==(Name left, Name right) => left.Equals(right);
    public static bool operator !=(Name left, Name right) => !left.Equals(right);
    public static bool operator <(Name left, Name right) => left.CompareTo(right) < 0;
    public static bool operator >(Name left, Name right) => left.CompareTo(right) > 0;
    public static bool operator <=(Name left, Name right) => left.CompareTo(right) <= 0;
    public static bool operator >=(Name left, Name right) => left.CompareTo(right) >= 0;
}