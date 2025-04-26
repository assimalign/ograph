namespace Assimalign.OGraph.Gdm;

/// <summary>
/// An edge links two nodes together.
/// </summary>
/// <remarks>
/// <i>An edge is also referred to as a Link.</i>
/// </remarks>
public interface IOGraphGdmEdge : IOGraphGdmLabeledElement
{
    //Label - ! The Edge Label must match a literal segment of on operation on the target vertex. Operation Methods must not be mismatched

    /// <summary>
    /// The target vertex.
    /// </summary>
    IOGraphGdmVertex Target { get; }

    /// <summary>
    /// The source vertex.
    /// </summary>
    IOGraphGdmVertex Source { get; }

    /// <summary>
    /// A collction of operation mappings.
    /// </summary>
    IOGraphGdmStepCollection Steps { get; }

    /// <summary>
    /// The graph in which the vertex belongs to.
    /// </summary>
    IOGraphGdmGraph Graph { get; }
}