using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmEdgeCollection : ICollection<IOGraphGdmEdge>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <param name="edge"></param>
    /// <returns></returns>
    bool TryGet(Label label, out IOGraphGdmEdge? edge);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    IOGraphGdmEdge GetOrLink(IOGraphGdmVertex source, IOGraphGdmVertex target);
}
