using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

public interface IOGraphVertexCollection : ICollection<IOGraphVertex>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Label"></param>
    /// <param name="node"></param>
    /// <returns></returns>
    bool TryGetVertex(Name label, out IOGraphVertex? node);
}
