using System;

namespace Assimalign.OGraph.Query;

public class MemberNode  : QueryNode
{
	public MemberNode(string name) : base (QueryNodeKind.Member)
	{
		if (string.IsNullOrEmpty(name))
		{
			throw new ArgumentException("The property", nameof(name));
		}
		this.Name = name;
	}

	public MemberNode(string name, MemberNode child) : this(name)
	{
		this.Child = child;
	}

	/// <summary>
	/// The name of the Entity Member being queried.
	/// </summary>
	public string Name { get; }
	public MemberNode? Child { get; }
	public override T Accept<T>(IQueryVisitor<T> visitor)
	{
		return base.Accept(visitor);
	}
}
