using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphPropertyCollection : IEnumerable<IOGraphProperty>
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
    /// <param name="property"></param>
    void Add(IOGraphProperty property);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="property"></param>
    void Remove(IOGraphProperty property);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="property"></param>
    /// <returns></returns>
    bool TryGet(Name name, out IOGraphProperty? property);
}
