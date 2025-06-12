namespace Assimalign.OGraph.Gdm;

/// <summary>
/// An edge links two nodes together.
/// </summary>
/// <remarks>
/// <i>An edge is also referred to as a Link.</i>
/// </remarks>
public interface IOGraphGdmEdge : IOGraphGdmBindableElement
{
    //Label - ! The Edge Label must match a literal segment of on operation on the target vertex. Operation Methods must not be mismatched

    /// <summary>
    /// The source vertex.
    /// </summary>
    IOGraphGdmNode Source { get; }

    /// <summary>
    /// The target vertex.
    /// </summary>
    IOGraphGdmNode Target { get; }

    /// <summary>
    /// The graph in which the vertex belongs to.
    /// </summary>
    IOGraphGdmGraph Graph { get; }
}