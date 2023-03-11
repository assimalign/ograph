using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

public interface IOGraphNodeCollection : IEnumerable<IOGraphNode>
{
    /// <summary>
    /// 
    /// </summary>
    int Count { get; }
    /// <summary>
    /// 
    /// </summary>
    bool IsReadOnly { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    void Add(IOGraphNode node);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    void Remove(IOGraphNode node);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    bool TryFind(Label label);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Label"></param>
    /// <param name="node"></param>
    /// <returns></returns>
    bool TryGet(Label Label, out IOGraphNode? node);

    
}
