using System.Collections.Generic;

namespace Assimalign.OGraph;

public interface IOGraphEdgeCollection : ICollection<IOGraphEdge>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <param name="edge"></param>
    /// <returns></returns>
    bool TryGet(Label label, out IOGraphEdge? edge);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    IOGraphEdge GetOrLink(IOGraphVertex source, IOGraphVertex target);
}
