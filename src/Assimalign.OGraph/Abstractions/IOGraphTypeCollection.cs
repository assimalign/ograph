using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;


public interface IOGraphTypeCollection : IEnumerable<IOGraphType>
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
    /// <param name="type"></param>
    void Add(IOGraphType type);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    void Remove(IOGraphType type);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    bool TryGet(Name name, out IOGraphType type);

}
