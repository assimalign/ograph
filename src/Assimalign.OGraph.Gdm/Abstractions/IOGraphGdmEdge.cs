using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// An edge links two nodes together.
/// </summary>
/// <remarks>
/// <i>An edge is also referred to as a Link.</i>
/// </remarks>
public interface IOGraphGdmEdge
{
    /// <summary>
    /// A unique name of the edge.
    /// </summary>
    Label Label { get; } //! The Edge Label must match a literal segment of on operation on the target vertex. Operation Methods must not be mismatched
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
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerable<IOGraphGdmBinding> GetBindings();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="binding"></param>
    /// <returns></returns>
    void AddBinding(IOGraphGdmInputBinding binding);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="binding"></param>
    /// <returns></returns>
    void AddBinding(IOGraphGdmOutputBinding binding);
}