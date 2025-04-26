using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmGraph : IOGraphGdmNamedElement
{
    /// <summary>
    /// A collection of types defined within the graph.
    /// </summary>
    IOGraphGdmTypeCollection Types { get; }

    /// <summary>
    /// A collection of edges within the graph.
    /// </summary>
    IOGraphGdmEdgeCollection Edges { get; }

    /// <summary>
    /// A collection of vertices within the graph.
    /// </summary>
    IOGraphGdmVertexCollection Vertices { get; }

    /// <summary>
    /// The root model.
    /// </summary>
    IOGraphGdm Model { get; }
}