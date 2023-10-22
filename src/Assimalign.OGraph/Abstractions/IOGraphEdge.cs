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
    Name Label { get; } //! The Edge Label must match the label of an operation. Operation Methods must not be mismatched
    /// <summary>
    /// The source vertex.
    /// </summary>
    IOGraphVertex Source { get; }
    /// <summary>
    /// The target is the node in which is linked to the source.
    /// </summary>
    IOGraphVertex Target { get; }
    /// <summary>
    /// Metadata for the edge.
    /// </summary>
    IOGraphMetadata Metadata { get; }
}