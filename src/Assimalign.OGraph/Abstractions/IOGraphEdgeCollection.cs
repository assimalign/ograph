using System.Collections.Generic;

namespace Assimalign.OGraph;

public interface IOGraphEdgeCollection : ICollection<IOGraphEdge>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="edge"></param>
    /// <returns></returns>
    bool TryGetEdge(Name name, out IOGraphEdge? edge);
}
