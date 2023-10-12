using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

public interface IOGraphNodeCollection : ICollection<IOGraphNode>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    bool TryFind(Name name);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Label"></param>
    /// <param name="node"></param>
    /// <returns></returns>
    bool TryGetNode(Name name, out IOGraphNode? node);
}
