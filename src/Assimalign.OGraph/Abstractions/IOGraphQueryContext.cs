using Assimalign.OGraph.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphQueryContext
{

    IOGraphNode Node { get;  }
    QueryDocument Query { get; }
}
