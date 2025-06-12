namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Represents a single entity and it's structure within the graph Model.
/// </summary>
/// <remarks>
/// A Node is also referred to as a Vertex.
/// </remarks>
public interface IOGraphGdmNode : IOGraphGdmNamedElement
{
    /// <summary>
    /// The type bound to this vertex.
    /// </summary>
    IOGraphGdmType Type { get; }

    /// <summary>
    /// The graph in which the vertex belongs to.
    /// </summary>
    IOGraphGdmGraph Graph { get; }
}