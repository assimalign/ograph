namespace Assimalign.OGraph.Query;

public class MemberNode  : QueryNode
{
	public MemberNode(string memberName) : base (QueryNodeKind.Member)
	{
		this.MemberName = memberName;
	}
	/// <summary>
	/// The name of the Entity Member being queried.
	/// </summary>
	public string MemberName { get; }

	public override T Accept<T>(IQueryVisitor<T> visitor)
	{
		return base.Accept(visitor);
	}
}
