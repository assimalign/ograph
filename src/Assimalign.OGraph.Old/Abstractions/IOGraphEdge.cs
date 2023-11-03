using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// An edge links two nodes together.
/// </summary>
/// <remarks>
/// <i>An edge is also referred to as a Link.</i>
/// </remarks>
public interface IOGraphEdge
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
    IOGraphVertex Source { get; }
    /// <summary>
    /// The target vertex.
    /// </summary>
    IOGraphVertex Target { get; }
    /// <summary>
    /// Metadata for the edge.
    /// </summary>
    IOGraphMetadata Metadata { get; }
}