using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Assimalign.OGraph;

/// <summary>
/// Represents a given HTTP method.
/// </summary>
public readonly struct Method : 
	IEquatable<Method>,
	IEqualityComparer<Method>
{
    private const string pattern = "^[a-zA-Z]+$"; // Alphabetic characters only
	public Method(string value)
	{
		if (string.IsNullOrEmpty(value))
		{
            throw new ArgumentNullException(nameof(value));
        }
		if (!Regex.IsMatch(value, pattern))
		{
            throw new Exception("Only Alphabetic characters are allowed as Method names");
        }
		Value = value.ToUpperInvariant();
	}

	/// <summary>
	/// 
	/// </summary>
	public string Value { get; }

	/// <inheritdoc />
	public override bool Equals([NotNullWhen(true)] object? instance)
	{
		if (instance is Method method)
		{
			return Equals(method);
		}
		return false;
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return HashCode.Combine(typeof(Method), Value);
	}

	/// <inheritdoc />
	public override string ToString()
	{
		return Value;
	}

	/// <inheritdoc />
	public bool Equals(Method other)
	{
		return Value.Equals(other.Value);
	}

	/// <inheritdoc />
	public bool Equals(Method left, Method right)
	{
		return left.Equals(right);
	}

	/// <inheritdoc />
	public int GetHashCode([DisallowNull] Method instance)
	{
		return instance.GetHashCode();
	}

	public static implicit operator Method(string value) => new Method(value);
	public static implicit operator string(Method method) => method.Value;



	public static Method Get => "GET";
	public static Method Post => "POST";
	public static Method Put => "PUT";
	public static Method Delete => "DELETE";
	public static Method Patch => "PATCH";
	public static Method Head => "HEAD";
	public static Method Options => "OPTIONS";
	public static Method Trace => "TRACE";
}
