using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Syntax;

public interface IOGraphQueryContext
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphNode Node { get;  }
    /// <summary>
    /// 
    /// </summary>
    QueryDocument Query { get; }
    /// <summary>
    /// 
    /// </summary>
    IServiceProvider ServiceProvider { get; }
}
