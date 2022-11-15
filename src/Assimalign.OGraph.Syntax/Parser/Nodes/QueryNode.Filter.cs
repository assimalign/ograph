using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public class FilterQueryNode : QueryNode
{
    public override QueryNodeType NodeType => QueryNodeType.Filter;
}
