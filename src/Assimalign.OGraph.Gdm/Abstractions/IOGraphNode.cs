using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Modeling;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// A Node is also referred to as a Vertex.
/// </remarks>
public interface IOGraphNode
{
    /// <summary>
    /// 
    /// </summary>
    bool IsRoot { get; }
    /// <summary>
    /// 
    /// </summary>
    NodeName Name { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphType Properties { get; }

    /// <summary>
    /// 
    /// </summary>
    IOGraphEdgeCollection Edges { get; set; }
}
