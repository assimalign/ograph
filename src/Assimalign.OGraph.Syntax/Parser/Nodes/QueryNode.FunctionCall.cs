using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed class FunctionCallQueryNode : QueryNode
{
    /// <summary>
    /// The name of the function being called.
    /// </summary>
    public string Name { get; init; }
    /// <summary>
    /// The function parameters
    /// </summary>
    public IEnumerable<QueryNode> Parameters { get; init; }

    public override QueryNodeType NodeType => QueryNodeType.Function;
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
