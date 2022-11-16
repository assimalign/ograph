using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed class MemberQueryNode : QueryNode
{
    /// <summary>
    /// Represents the name of the member.
    /// </summary>
    public string Name { get; init; }
    public override QueryNodeType NodeType => QueryNodeType.Member;
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
