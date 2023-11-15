using System;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public readonly struct QueryValue
{
	public QueryValue(string value)
	{
		this.Value = value;
	}

	public string Value { get; }

	/// <summary>
	/// Returns the raw value as a <see cref="ReadOnlySpan{T}"/>.
	/// </summary>
	/// <returns></returns>
	public ReadOnlySpan<byte> GetBytes() => Encoding.UTF8.GetBytes(Value);


	public static implicit operator QueryValue(string value) => new QueryValue(value);
	public static implicit operator string(QueryValue value) => value.Value;
}
