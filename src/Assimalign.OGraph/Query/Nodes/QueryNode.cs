using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Query;

public abstract class QueryNode
{
    public QueryNode() { }
    public QueryNode(QueryNodeKind nodeKind)
    {
        NodeKind = nodeKind;
    }

    public virtual QueryNodeKind NodeKind { get; }
    public virtual T Accept<T>(IQueryVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
