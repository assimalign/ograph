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
    bool TryGetEdge(Name label, out IOGraphEdge? edge);

    bool TryAddEdge(IOGraphEdge edge);
}
