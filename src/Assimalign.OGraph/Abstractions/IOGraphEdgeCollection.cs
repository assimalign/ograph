using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphEdgeCollection : IEnumerable<IOGraphEdge>
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
    /// <param name="edge"></param>
    void Add(IOGraphEdge edge);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="edge"></param>
    void Remove(IOGraphEdge edge);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="edge"></param>
    /// <returns></returns>
    bool TryGet(Name name, out IOGraphEdge? edge);
}
