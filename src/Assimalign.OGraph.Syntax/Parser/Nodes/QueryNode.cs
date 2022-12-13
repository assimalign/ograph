namespace Assimalign.OGraph.Syntax; 

public abstract class QueryNode
{
    /// <summary>
    /// An identifier for the node type.
    /// </summary>
    public abstract QueryNodeType NodeType { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="visitor"></param>
    /// <returns></returns>
    public virtual T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
