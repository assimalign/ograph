using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class MemberQueryNode : QueryNode
{
    ~MemberQueryNode() { }
    public MemberQueryNode(string name)
    {
        this.Name = name;
    }

    /// <summary>
    /// Represents the name of the member.
    /// </summary>
    public string Name { get; }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Member;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
