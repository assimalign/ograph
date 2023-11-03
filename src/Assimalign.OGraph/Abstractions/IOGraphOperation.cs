using Assimalign.OGraph.Gdm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphOperation : IOGraphGdmVertexBinding
{
    /// <summary>
    /// 
    /// </summary>
    Label Label { get; }
    /// <summary>
    /// 
    /// </summary>
    Route Route { get; }
    /// <summary>
    /// 
    /// </summary>
    Method Method { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphOperationResolver Resolver { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphOperationMiddlewareQueue Middleware { get; }
    /// <summary>
    /// Builds a handler that create invocation chain to execute middleware and resolver.
    /// </summary>
    /// <returns></returns>
    OGraphOperationHandler GetHandlerChain();
}