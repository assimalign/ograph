using System;
using System.Text;
using System.Diagnostics;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
[DebuggerDisplay("{Value}")]
public readonly struct QueryValue : IEquatable<QueryValue>
{
	public QueryValue(string value)
	{
		this.Value = value;
	}

	/// <summary>
	/// The raw query value.
	/// </summary>
	public string Value { get; }
	/// <summary>
	/// Returns the Value as a read-only span of bytes.
	/// </summary>
	/// <param name="encoding">Default is UTF8</param>
	/// <returns></returns>
	public ReadOnlySpan<byte> GetBytes(Encoding? encoding = null) => (encoding ?? Encoding.UTF8).GetBytes(Value);

	/// <inheritdoc />
    public override string ToString()
    {
		return Value;
    }

    /// <inheritdoc />
    public override bool Equals(object? instance)
    {
        if (instance is QueryValue queryValue)
		{
			return Equals(queryValue);
		}
		return false;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
		return HashCode.Combine(typeof(QueryValue), Value);
    }

    /// <inheritdoc />
    public bool Equals(QueryValue other)
    {
		return Value.Equals(other.Value);
    }

    public static implicit operator QueryValue(string value) => new QueryValue(value);
	public static implicit operator string(QueryValue value) => value.Value;
}
