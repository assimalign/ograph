using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// An edge links to nodes together.
/// </summary>
/// <remarks>
/// An Edge is also referred to as a Link.
/// </remarks>
public interface IOGraphEdge
{
    /// <summary>
    /// The name of the Edge.
    /// </summary>
    Name Name { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphNode SourceNode { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphNode TargetNode { get; }    
    /// <summary>
    /// 
    /// </summary>
    IOGraphEdgeResolver EdgeResolver { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphEdgeMiddlewareCollection Middleware { get; }
}
