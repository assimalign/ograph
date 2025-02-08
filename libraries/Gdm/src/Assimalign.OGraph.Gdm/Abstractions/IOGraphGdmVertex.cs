namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Represents a single entity and it's structure within the graph Model.
/// </summary>
/// <remarks>
/// A Node is also referred to as a Vertex.
/// </remarks>
public interface IOGraphGdmVertex : IOGraphGdmLabeledElement
{
    /// <summary>
    /// The type bound to this vertex.
    /// </summary>
    IOGraphGdmType Type { get; }

    /// <summary>
    /// A collection of operations defined for the vertex
    /// </summary>
    IOGraphGdmOperationCollection Operations { get; }

    /// <summary>
    /// A collection of edges that are connected to this node.
    /// </summary>
    IOGraphGdmEdgeCollection Edges { get; }

    /// <summary>
    /// The graph in which the vertex belongs to.
    /// </summary>
    IOGraphGdmGraph Graph { get; }
}