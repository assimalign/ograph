using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class BinaryQueryNode : QueryNode
{
    internal BinaryQueryNode() { }
    public BinaryQueryNode(QueryNode left, QueryNode right, BinaryOperatorType operatorType)
    {
        LeftOperand = left;
        RightOperand = right;
        OperatorType = operatorType;
    }
    /// <summary>
    /// 
    /// </summary>
    public QueryNode? LeftOperand { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public QueryNode? RightOperand { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public BinaryOperatorType OperatorType { get; init; }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Binary;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }

    /// <inheritdoc />
    public override IEnumerable<TNode> GetNodesOfType<TNode>()
    {
        if (this is TNode node1)
        {
            yield return node1;
        }
        if (LeftOperand is not null)
        {
            foreach (var node2 in LeftOperand.GetNodesOfType<TNode>())
            {
                yield return node2;
            }
        }
        if (RightOperand is not null)
        {
            foreach (var node3 in RightOperand.GetNodesOfType<TNode>())
            {
                yield return node3;
            }
        }
    }
}
