using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// An edge links two nodes together.
/// </summary>
/// <remarks>
/// <i>An edge is also referred to as a Link.</i>
/// </remarks>
public interface IOGraphGdmEdge : IOGraphGdmBindingElement
{
    //Label - ! The Edge Label must match a literal segment of on operation on the target vertex. Operation Methods must not be mismatched
    
    
    /// <summary>
    /// Gets the cardinality of the Target 
    /// Vertex/Vertices being resolved.
    /// </summary>
    CardinalityType Cardinality { get; }
    /// <summary>
    /// The source vertex.
    /// </summary>
    IOGraphGdmVertexReference Source { get; }
    /// <summary>
    /// The target vertex.
    /// </summary>
    IOGraphGdmVertexReference Target { get; }
    /// <summary>
    /// Metadata for the edge.
    /// </summary>
    IOGraphGdmMetadata Metadata { get; }
}