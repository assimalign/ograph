using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph;

/// <summary>
/// Represents a name value with the standard naming convention for OGraph objects.
/// </summary>
public readonly struct Name : IEquatable<Name>, IEqualityComparer<Name>
{
    private const string allowedCharacters = "abcdefghijklmnopqrstuvwxwzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567980";

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

    public override bool Equals([NotNullWhen(true)] object? instance)
    {
        if (instance is not  Name  name)
        {
            return false;
        }

        return this.Equals(name);
    }
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public bool Equals(Name other)
    {
        return this.Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
    }

    public bool Equals(Name left, Name right)
    {
        return left.Equals(right);
    }

    public int GetHashCode([DisallowNull] Name instance)
    {
        return instance.GetHashCode();
    }

    public static implicit operator Name(string value) => new Name(value);
    public static implicit operator string(Name name) => name.Value;
}