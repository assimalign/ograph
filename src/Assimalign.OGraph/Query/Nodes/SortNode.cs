using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Query;

public class SortNode : QueryNode
{

    public SortNode() : base (QueryNodeKind.Sort)
    {

    }


    public SortNode ThenBy { get; }

    public SortNodeKind SortKind { get; }
}
