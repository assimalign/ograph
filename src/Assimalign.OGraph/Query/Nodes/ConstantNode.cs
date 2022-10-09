namespace Assimalign.OGraph.Query;


/// <summary>
/// A <see cref="ConstantNode"/> represents a value type.
/// </summary>
public class ConstantNode : QueryNode
{
	public ConstantNode(object value) : base(QueryNodeKind.Constant)
	{
		this.Value = value;
	}
	public object Value { get; }
	public override T Accept<T>(IQueryVisitor<T> visitor)
	{
		return base.Accept(visitor);
	}
}
