using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmGraph : IOGraphGdmBindableElement
{
    /// <summary>
    /// A collection of edges within the graph.
    /// </summary>
    IEnumerable<IOGraphGdmEdge> Edges { get; }

    /// <summary>
    /// A collection of vertices within the graph.
    /// </summary>
    IEnumerable<IOGraphGdmVertex> Vertices { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphGdmEdge GetEdge(Label label);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphGdmVertex GetVertex(Label label);
}
