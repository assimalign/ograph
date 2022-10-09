using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Query;

public sealed class RootNode : QueryNode
{
    public RootNode() : base (QueryNodeKind.Root)
    {

    }

    public FilterNode Filter { get; }
    public SelectNode Select { get; }
    public SortNode Sort { get; }

}
