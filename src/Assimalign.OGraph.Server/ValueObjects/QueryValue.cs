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

	/// <summary>
	/// 
	/// </summary>
	public string Value { get; }
	/// <summary>
	/// 
	/// </summary>
	/// <param name="encoding"></param>
	/// <returns></returns>
	public ReadOnlySpan<byte> GetBytes(Encoding? encoding = null) => (encoding ?? Encoding.UTF8).GetBytes(Value);


	public static implicit operator QueryValue(string value) => new QueryValue(value);
	public static implicit operator string(QueryValue value) => value.Value;
}
