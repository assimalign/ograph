using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph;

/// <summary>
/// Represents a name value with the standard naming convention for OGraph objects.
/// </summary>
public readonly struct Label : IEquatable<Label>, IEqualityComparer<Label>
{
    private const string allowedCharacters = "abcdefghijklmnopqrstuvwxwzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567980";

    public Label(string value)
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
        if (instance is not  Label  name)
        {
            return false;
        }

        return this.Equals(name);
    }
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public bool Equals(Label other)
    {
        return this.Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
    }

    public bool Equals(Label left, Label right)
    {
        return left.Equals(right);
    }

    public int GetHashCode([DisallowNull] Label instance)
    {
        return instance.GetHashCode();
    }

    public static implicit operator Label(string value) => new Label(value);
    public static implicit operator string(Label name) => name.Value;
}