using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Query;

public class SortNode : QueryNode
{

    public SortNode(MemberNode memberNode) : base (QueryNodeKind.Sort)
    {

    }

    public MemberNode Member { get; }
    public SortNode ThenBy { get; }
    public SortNodeKind SortKind { get; } = SortNodeKind.Ascending;
    public override T Accept<T>(IQueryVisitor<T> visitor)
    {
        return base.Accept(visitor);
    }
}
