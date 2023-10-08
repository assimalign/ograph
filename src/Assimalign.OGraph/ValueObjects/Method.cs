using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph;

/// <summary>
/// Represents a given HTTP method.
/// </summary>
public readonly struct Method : 
	IEquatable<Method>,
	IEqualityComparer<Method>
{
	private const string allowedCharacters = "abcdefghijklmnopqrstuvwxwzABCDEFGHIJKLMNOPQRSTUVWXYZ";
	public Method(string value)
	{
		if (string.IsNullOrEmpty(value))
		{
            throw new ArgumentNullException(nameof(value));
        }
		if (value.Any(character => !allowedCharacters.Contains(character)))
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
		return base.GetHashCode();
	}

	/// <inheritdoc />
	public override string ToString()
	{
		return Value;
	}

	/// <inheritdoc />
	public bool Equals(Method other)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	public bool Equals(Method left, Method right)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	public int GetHashCode([DisallowNull] Method instance)
	{
		throw new NotImplementedException();
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
