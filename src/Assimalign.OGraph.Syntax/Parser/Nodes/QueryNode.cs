using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax; 


public abstract class QueryNode
{
    /// <summary>
    /// An identifier for the node type.
    /// </summary>
    public abstract QueryNodeType NodeType { get; }
    public virtual T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
