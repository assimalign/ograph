namespace Assimalign.OGraph.Query;

public class BinaryNode : QueryNode
{
    public BinaryNode(QueryNode left, QueryNode right, BinaryNodeKind binaryKind) 
        : base (QueryNodeKind.Binary)
    {
        this.Left = left;
        this.Right = right;
        this.BinaryKind = binaryKind;
    }
    public QueryNode Left { get; }
    public QueryNode Right { get; }
    public BinaryNodeKind BinaryKind { get; }
    public override T Accept<T>(IQueryVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
