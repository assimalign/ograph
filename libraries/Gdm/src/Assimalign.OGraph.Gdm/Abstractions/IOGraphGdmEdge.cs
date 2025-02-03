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
    /// The source vertex.
    /// </summary>
    IOGraphGdmVertexReference Source { get; }

    /// <summary>
    /// The target vertex.
    /// </summary>
    IOGraphGdmVertexReference Target { get; }

    /// <summary>
    /// Get the collection of operations for this edge.
    /// </summary>
    IOGraphGdmOperationCollection Operations { get; }
}