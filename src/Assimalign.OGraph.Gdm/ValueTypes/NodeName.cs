using System;

namespace Assimalign.OGraph.Modeling;

public readonly struct NodeName : IEquatable<NodeName>
{
	public NodeName(string value)
	{
		if (string.IsNullOrEmpty(value))
		{
			throw new ArgumentNullException(nameof(value));
		}
        
		this.Value = value;
	}

	public string Value { get; }

    public bool Equals(NodeName other) => string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);

    public static implicit operator NodeName(string value) => new NodeName(value);
	public static implicit operator String(NodeName name) => name.Value;
}
