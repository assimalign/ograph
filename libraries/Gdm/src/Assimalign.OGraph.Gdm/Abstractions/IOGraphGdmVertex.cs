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
    IOGraphGdmTypeReference Type { get; } // TODO: Revisit whether IOGraphEntityType should be specified explicitly

    /// <summary>
    /// A collection of edges that are connected to this node.
    /// </summary>
    IOGraphGdmEdgeReferenceCollection Edges { get; }

    /// <summary>
    /// A collection of operations defined for the vertex
    /// </summary>
    IOGraphGdmOperationCollection Operations { get; }
}