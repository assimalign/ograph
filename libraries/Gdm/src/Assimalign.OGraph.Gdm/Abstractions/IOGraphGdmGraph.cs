using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmGraph : IOGraphGdmBindingElement
{
    /// <summary>
    /// 
    /// </summary>
    IEnumerable<IOGraphGdmEdge> Edges { get; }
    /// <summary>
    /// 
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
